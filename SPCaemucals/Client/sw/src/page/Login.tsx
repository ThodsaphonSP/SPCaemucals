
import React, {useState} from "react";
import {useNavigate} from "react-router-dom";
import {
    Box, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle,
    FormControl,
    Grid, IconButton,
    InputAdornment,
    InputLabel,
    OutlinedInput,
    Paper
} from "@mui/material";
import {Visibility, VisibilityOff} from "@mui/icons-material";
import {LoginCredentials} from "../type/authTypes";

import { login} from "../features/auth/authSlice";
import { useAppDispatch } from "../app/hooks"
import { Button } from "@mui/material";

export function Login(){


    const navigate = useNavigate();

    const [credentials, setCredentials] = useState<LoginCredentials>({ emailOrPhone:"",password:"",rememberMe:false});
    const dispatch = useAppDispatch();

    const [isDialogOpen, setIsDialogOpen] = useState(false);
    const [dialogContent, setDialogContent] = useState({ title: '', text: '', status: '' });


    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        dispatch(login(credentials))

            .then((result) => {
                if (login.fulfilled.match(result)) {
                    setDialogContent({ title: 'Info', text: 'Login Successful', status: 'success' });
                    debugger
                    setIsDialogOpen(true);
                    navigate('/'); // on login success, redirect to "/dashboard"
                } else if (login.rejected.match(result)) {
                    setDialogContent({ title: 'Error', text: result.payload ? result.payload.toString() : 'Login Failed', status: 'error' });
                    setIsDialogOpen(true);
                }
            });
    };
    
    const [showPassword, setShowPassword] = React.useState(false);

    const handleClickShowPassword = () => setShowPassword((show) => !show);
    const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
    };

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setCredentials({
            ...credentials,
            [event.target.name]: event.target.value,
        });
    };

    const handleClose = () => {
        setIsDialogOpen(false);
    };





    return (

        <div>

            <Dialog
                open={isDialogOpen}
                onClose={handleClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">{dialogContent.title}</DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        {dialogContent.text}
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="primary" autoFocus>
                        Close
                    </Button>
                </DialogActions>
            </Dialog>

            <form onSubmit={handleSubmit}>
                <Box sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    minHeight: '100vh',
                    backgroundColor: "#2E3192"
                }}>

                    <Grid container spacing={0} direction="column" alignItems="center" justifyContent="center"
                          style={{minHeight: '100vh'}}>
                        <Grid item xs={3}>
                            <Paper>
                                <Grid direction={"column"} justifyItems={"center"} rowSpacing={2}
                                      sx={{padding: "40px"}} container alignContent={"center"}>

                                    <Grid xs={12} item={true}>
                                        <InputLabel htmlFor="emailOrPhone">Email Or Phone</InputLabel>
                                        <FormControl size={"small"} fullWidth={true} sx={{m: 1, width: '25ch'}}
                                                     variant="outlined">
                                            <OutlinedInput id="emailOrPhone" name="emailOrPhone"
                                                           value={credentials.emailOrPhone} onChange={handleInputChange}/>
                                        </FormControl>
                                    </Grid>

                                    <Grid xs={12} item={true}>
                                        <InputLabel htmlFor="password">Password</InputLabel>
                                        <FormControl size={"small"} fullWidth={true} sx={{m: 1, width: '25ch'}}
                                                     variant="outlined">
                                            <OutlinedInput
                                                id="password"
                                                name="password"
                                                type={showPassword ? 'text' : 'password'}
                                                value={credentials.password}
                                                onChange={handleInputChange}
                                                endAdornment={
                                                    <InputAdornment position="end">
                                                        <IconButton
                                                            aria-label="toggle password visibility"
                                                            onClick={handleClickShowPassword}
                                                            onMouseDown={handleMouseDownPassword}
                                                            edge="end"
                                                        >
                                                            {showPassword ? <VisibilityOff/> : <Visibility/>}
                                                        </IconButton>
                                                    </InputAdornment>
                                                }
                                            />
                                        </FormControl>

                                    </Grid>
                                    <Grid item={true} xs={12}>
                                        <Button fullWidth={true} variant={"contained"} type="submit">Login</Button>
                                    </Grid>
                                </Grid>
                            </Paper>
                        </Grid>
                    </Grid>
                </Box>
            </form>
        </div>

    )
}