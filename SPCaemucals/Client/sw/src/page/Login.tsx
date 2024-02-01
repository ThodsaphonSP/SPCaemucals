
import React from "react";
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

export function Login (){

    const [showPassword, setShowPassword] = React.useState(false);
    const handleClickShowPassword = () => setShowPassword((show) => !show);
    const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
    };
    return (<>
        <Box sx={{
            display: 'flex',
            flexDirection: 'column',
            minHeight: '100vh',
            backgroundColor:"#2E3192"
        }}>


            <Grid
                container
                spacing={0}
                direction="column"
                alignItems="center"
                justifyContent="center"
                style={{ minHeight: '100vh' }}
            >
                <Grid  item xs={3}>
                    <Paper>
                        <Grid rowSpacing={2} direction={"column"} alignContent={"center"} justifyItems={"center"} sx={{padding:"40px"}}   container={true}>

                            <Grid xs={12} item={true}>
                                <InputLabel htmlFor="outlined-basic">เบอร์โทรศัพท์</InputLabel>
                                <FormControl size={"small"} fullWidth={true} sx={{ m: 1, width: '25ch' }} variant="outlined">
                                    <OutlinedInput id={"phonNO"}/>

                                </FormControl>

                            </Grid>
                            <Grid xs={12} item={true}>
                                <InputLabel htmlFor="outlined-adornment-password">Password</InputLabel>
                                <FormControl size={"small"} fullWidth={true} sx={{ m: 1, width: '25ch' }} variant="outlined">

                                    <OutlinedInput
                                        id="outlined-adornment-password"
                                        type={showPassword ? 'text' : 'password'}
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

                            </Grid>
                        </Grid>
                    </Paper>
                </Grid>
            </Grid>
        </Box>

    </>)
}