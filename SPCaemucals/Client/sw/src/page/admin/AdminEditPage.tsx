import React, {ReactNode, useState} from 'react';
import { BaseContainer } from './BaseContainer'; // Assuming this is a custom component
import { Grid, TextField, Button, Typography } from '@mui/material';

export interface BaseContainerProps {
    children: ReactNode;
}


export function AdminEditPage() {
    // State for user form
    const [user, setUser] = useState({
        name: '',
        username: '',
        phoneNumber: '',
        email: '',
        role: ''
    });

    // Handle input change
    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;
        setUser(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    // Handle form submit
    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        console.log('User Data:', user);
        // Here you would typically make your API call to update the user
    };

    return (
        <BaseContainer>
            <Grid container spacing={2} component="form" onSubmit={handleSubmit} sx={{ padding: 2 }}>
                <Grid item xs={12}>
                    <Typography variant="h6">Edit User</Typography>
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        fullWidth
                        label="Name"
                        name="name"
                        value={user.name}
                        onChange={handleChange}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        fullWidth
                        label="Username"
                        name="username"
                        value={user.username}
                        onChange={handleChange}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        fullWidth
                        label="Phone Number"
                        name="phoneNumber"
                        value={user.phoneNumber}
                        onChange={handleChange}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        fullWidth
                        label="Email"
                        name="email"
                        value={user.email}
                        onChange={handleChange}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        fullWidth
                        label="Role"
                        name="role"
                        value={user.role}
                        onChange={handleChange}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Button type="submit" variant="contained">Submit</Button>
                </Grid>
            </Grid>
        </BaseContainer>
    );
}
