import {
    AppBar,
    Avatar,
    Box,
    Container,
    Grid,
    IconButton,
    Menu,
    MenuItem,
    Toolbar,
    Tooltip,
    Typography
} from "@mui/material";

import AdbIcon from '@mui/icons-material/Adb';
import {SideManu} from "../../component/SideManu";
import MenuIcon from '@mui/icons-material/Menu';
import React, {useEffect, useState} from "react";
import {useSelector} from "react-redux";
import {selectNavTitle, selectUser} from "../../app/store";
import {User} from "../../type/User";
import {useAppSelector} from "../../app/hooks";
import {Outlet, useNavigate} from "react-router-dom";

export function GetName(user: User | null) {

    if (user){
        return `${user.firstName}  ${user.lastName}`;
    }else {
        return "";
    }
}

function GetCompanyName(user: User | null) {

    if (user){
        return `${user.company.companyName}`;
    }else {
        return "";
    }
}

export function MainPage() {

    const navigate = useNavigate();
    useEffect(()=>{
        navigate("/admin")
    },[navigate])

    const title = useAppSelector(selectNavTitle);


    const [anchorElNav, setAnchorElNav] = useState<null | HTMLElement>(null);
    const pages = ['Products', 'Pricing', 'Blog'];
    const [anchorElUser, setAnchorElUser] = useState<null | HTMLElement>(null);
    const settings = ['Profile', 'Account', 'Dashboard', 'Logout'];

    const user = useSelector(selectUser);

    const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElNav(event.currentTarget);
    };

    const handleCloseNavMenu = () => {
        setAnchorElNav(null);
    };

    const handleCloseUserMenu = () => {
        setAnchorElUser(null);
    };

    const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElUser(event.currentTarget);
    };

    return (
        <>
            <Grid container style={{height: '100vh'}}>
                <Grid id={"sideBar"} item sm={'auto'} sx={{display: {xs: "none", sm: "flex"}}} style={{height: '100%'}}>
                    <SideManu/>
                </Grid>
                <Grid item={true} xs={12} sm>
                    <Grid direction={"column"} container={true}>
                        <Grid item={true} xs={12}>
                            <AppBar position="static" sx={{backgroundColor: "white"}}>
                                <Container maxWidth="xl">
                                    <Toolbar disableGutters>
                                        {/*<AdbIcon sx={{display: {xs: 'none', md: 'flex'}, mr: 1}}/>*/}
                                        <Grid container={true}>
                                            <Grid item={true} xs>
                                                <Typography
                                                    variant="h6"
                                                    noWrap
                                                    component="a"
                                                    href="#app-bar-with-responsive-menu"
                                                    sx={{
                                                        mr: 2,
                                                        display: {xs: 'none', md: 'flex'},
                                                        fontWeight: 700,
                                                        color: 'black',
                                                        textDecoration: 'none',
                                                    }}
                                                >
                                                    {title}
                                                </Typography>

                                                <Box sx={{
                                                    color: "black",
                                                    flexGrow: 1,
                                                    display: {xs: 'flex', md: 'none'}
                                                }}>
                                                    <IconButton
                                                        size="large"
                                                        aria-label="account of current user"
                                                        aria-controls="menu-appbar"
                                                        aria-haspopup="true"
                                                        onClick={handleOpenNavMenu}
                                                        color="inherit"
                                                    >
                                                        <MenuIcon/>
                                                    </IconButton>
                                                    <Menu
                                                        id="menu-appbar"
                                                        anchorEl={anchorElNav}
                                                        anchorOrigin={{
                                                            vertical: 'bottom',
                                                            horizontal: 'left',
                                                        }}
                                                        keepMounted
                                                        transformOrigin={{
                                                            vertical: 'top',
                                                            horizontal: 'left',
                                                        }}
                                                        open={Boolean(anchorElNav)}
                                                        onClose={handleCloseNavMenu}
                                                        sx={{
                                                            display: {xs: 'block', md: 'none'},
                                                        }}
                                                    >
                                                        {pages.map((page) => (
                                                            <MenuItem key={page} onClick={handleCloseNavMenu}>
                                                                <Typography textAlign="center">{page}</Typography>
                                                            </MenuItem>
                                                        ))}
                                                    </Menu>
                                                </Box>
                                                <AdbIcon sx={{display: {xs: 'flex', md: 'none'}, mr: 1}}/>
                                                <Typography
                                                    variant="h5"
                                                    noWrap
                                                    component="a"
                                                    href="#app-bar-with-responsive-menu"
                                                    sx={{
                                                        mr: 2,
                                                        display: {xs: 'flex', md: 'none'},
                                                        flexGrow: 1,
                                                        fontWeight: 700,
                                                        letterSpacing: '.3rem',
                                                        color: 'black',
                                                        textDecoration: 'none',
                                                    }}
                                                >
                                                    {title}
                                                </Typography>
                                            </Grid>
                                            <Grid item={true} xs={'auto'}>
                                                <Tooltip title="Open settings">
                                                    <Grid container justifyContent="flex-start" columnSpacing={2}>
                                                        <Grid item>
                                                            <Typography
                                                                sx={{color: "black"}}>{GetName(user)}</Typography>
                                                            <Typography
                                                                sx={{color: "black"}}>{GetCompanyName(user)}</Typography>
                                                        </Grid>
                                                        <Grid item>
                                                            <IconButton onClick={handleOpenUserMenu} sx={{p: 0}}>
                                                                <Avatar alt="Remy Sharp"
                                                                        src="/static/images/avatar/2.jpg"/>
                                                            </IconButton>
                                                        </Grid>
                                                    </Grid>

                                                </Tooltip>
                                                <Menu
                                                    sx={{mt: '45px'}}
                                                    id="menu-appbar"
                                                    anchorEl={anchorElUser}
                                                    anchorOrigin={{
                                                        vertical: 'top',
                                                        horizontal: 'right',
                                                    }}
                                                    keepMounted
                                                    transformOrigin={{
                                                        vertical: 'top',
                                                        horizontal: 'right',
                                                    }}
                                                    open={Boolean(anchorElUser)}
                                                    onClose={handleCloseUserMenu}
                                                >
                                                    {settings.map((setting) => (
                                                        <MenuItem key={setting} onClick={handleCloseUserMenu}>
                                                            <Typography textAlign="center">{setting}</Typography>
                                                        </MenuItem>
                                                    ))}
                                                </Menu>
                                            </Grid>
                                        </Grid>


                                    </Toolbar>
                                </Container>
                            </AppBar>
                        </Grid>
                        <Grid item={true} xs={12}>
                            <Outlet></Outlet>
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>
        </>
    );
}