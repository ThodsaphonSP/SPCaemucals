import React, {ReactNode, useEffect, useState} from 'react';
import { BaseContainer } from './BaseContainer'; // Assuming this is a custom component
import {
    Grid,
    TextField,
    Button,
    Typography,
    FormControl,
    InputLabel,
    Select,
    OutlinedInput,
    MenuItem,
    Checkbox,
    ListItemText, SelectChangeEvent
} from '@mui/material';
import {setTitle} from "../../features/Nav/NavSlice";
import pageData from "../../type/PageData.json";
import {useDispatch} from "react-redux";
import {Role} from "../../type/Role";
import {GetRoleAPI} from "../../Services/RoleAPI";

export interface BaseContainerProps {
    children: ReactNode;
}



const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
    PaperProps: {
        style: {
            maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
            width: 250,
        },
    },
};



export function AdminEditPage() {

    const [roleLoading, setRoleLoading] = useState(false);

    const [roles, setRoles] = useState<Role[]>([]);

    // State for user form
    const [user, setUser] = useState({
        name: '',
        username: '',
        phoneNumber: '',
        email: '',
        role: ''
    });

    const [roleName, setRoleName] = React.useState<string[]>([]);


    const dispatch = useDispatch();

    useEffect(() => {

        const titleValue = pageData.adminEdit.pageTitle;
        dispatch(setTitle(titleValue));
        document.title = titleValue;


    }, [dispatch]);


    useEffect(() => {
        fetchRoleData().catch(error=>{
            console.error('Failed to fetch data:', error);
        })
    }, []);

    const handleSelectChange = (event: SelectChangeEvent<typeof roleName>) => {
        let value: string[] | string;
        ({
            target: {value},
        } = event);

        const a = typeof value === 'string'
        debugger
        setRoleName(
            typeof value === 'string' ? value.split(',') : value,
        );
    };

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


    const fetchRoleData = async (name = ""): Promise<void> => {
        setRoleLoading(true);
        try {
            const response = await GetRoleAPI(name);

            setRoles(response.data);

            const names = response.data.map(x=>x.name);

            setRoleName(names);

        } catch (error) {
            console.error('Failed to fetch data:', error);
        }
        setRoleLoading(false);
    };

    return (
        <BaseContainer>
            <Grid rowSpacing={2} container component="form" onSubmit={handleSubmit}>
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
                    <FormControl fullWidth={true} >
                        <InputLabel id="demo-multiple-checkbox-label">Tag</InputLabel>
                        <Select
                            labelId="demo-multiple-checkbox-label"
                            id="demo-multiple-checkbox"
                            multiple
                            value={roleName}
                            onChange={handleSelectChange}
                            input={<OutlinedInput label="Tag" />}
                            renderValue={(selected) => selected.join(', ')}
                            MenuProps={MenuProps}
                        >
                            {roles.map((role) => (
                                <MenuItem key={role.id} value={role.name}>
                                    <Checkbox checked={roleName.indexOf(role.name) > -1} />
                                    <ListItemText primary={role.name} />
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                </Grid>
                <Grid container={true} item xs={12}>
                    <Grid xs>
                        <Button
                            type="submit"
                            variant="contained"
                            sx={{
                                backgroundColor: "#1E90FF",
                                color: "#FFFFFF",
                                '&:hover': {
                                    backgroundColor: '#0000CD',
                                },
                            }}
                        >
                            ย้อนกลับ
                        </Button>
                    </Grid>
                    <Grid xs>
                        <Button
                            type="submit"
                            variant="contained"
                            sx={{
                                backgroundColor: "#FF6347", //Change this to preferred background color
                                color: "#FFFFFF", //Change this to preferred text color
                                '&:hover': {
                                    backgroundColor: '#FF4500', //Change this to preferred hover color
                                },
                            }}
                        >
                            ลบ
                        </Button>
                    </Grid>
                    <Grid>

                        <Button
                            type="submit"
                            variant="contained"
                            sx={{
                                backgroundColor: "#32CD32",
                                color: "#FFFFFF",
                                '&:hover': {
                                    backgroundColor: '#228B22',
                                },
                            }}
                        >
                            บันทึก
                        </Button>
                    </Grid>
                </Grid>
            </Grid>
        </BaseContainer>
    );
}
