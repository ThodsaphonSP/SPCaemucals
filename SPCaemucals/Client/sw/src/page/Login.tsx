
import React, {useEffect, useState} from "react";
import {useLocation, useNavigate} from "react-router-dom";
import {
    Box,
    FormControl,
    Grid, IconButton,
    InputAdornment,
    InputLabel,
    OutlinedInput,
    Paper
} from "@mui/material";
import {Visibility, VisibilityOff} from "@mui/icons-material";
import {LoginCredentials} from "../type/authTypes";

import {getCookie, login, reLogin} from "../features/auth/authSlice";
import { useAppDispatch } from "../app/hooks"
import { Button } from "@mui/material";

export function Login(){


    const navigate = useNavigate();

    const [credentials, setCredentials] = useState<LoginCredentials>({ email: '', password: '',twoFactorCode:"",twoFactorRecoveryCode:"" });
    const dispatch = useAppDispatch();




    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        dispatch(login(credentials)).then(() => {
            navigate('/');   // on login success, redirect to "/dashboard"
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





    return (
        <form onSubmit={handleSubmit}>
            <Box sx={{
                display: 'flex',
                flexDirection: 'column',
                minHeight: '100vh',
                backgroundColor: "#2E3192"
            }}>

                <Grid container spacing={0} direction="column" alignItems="center" justifyContent="center"
                      style={{ minHeight: '100vh' }}>
                    <Grid  item xs={3}>
                        <Paper>
                            <Grid direction={"column"} justifyItems={"center"} rowSpacing={2}
                                  sx={{padding:"40px"}} container alignContent={"center"}>

                                <Grid xs={12} item={true}>
                                    <InputLabel htmlFor="email">Username</InputLabel>
                                    <FormControl size={"small"} fullWidth={true} sx={{ m: 1, width: '25ch' }} variant="outlined">
                                        <OutlinedInput id="email" name="email"
                                                       value={credentials.email} onChange={handleInputChange} />
                                    </FormControl>
                                </Grid>

                                <Grid xs={12} item={true}>
                                    <InputLabel htmlFor="password">Password</InputLabel>
                                    <FormControl size={"small"} fullWidth={true} sx={{ m: 1, width: '25ch' }} variant="outlined">
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
                                                        {showPassword ? <VisibilityOff /> : <Visibility />}
                                                    </IconButton>
                                                </InputAdornment>
                                            }
                                        />
                                    </FormControl>
                                    <Button type="submit">Login</Button>
                                </Grid>
                            </Grid>
                        </Paper>
                    </Grid>
                </Grid>
            </Box>
        </form>
    )
}