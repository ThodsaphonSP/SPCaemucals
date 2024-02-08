import {
    Pagination,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Select,
    MenuItem,
    FormControl,
    InputLabel,
    SelectChangeEvent, Grid, Button, TextField, Skeleton
} from "@mui/material";
import React, {useEffect, useState} from "react";
import {useDispatch} from "react-redux";
import {setTitle} from "../../features/Nav/NavSlice";
import pageData from "../../type/PageData.json"
import {User} from "../../type/User";
import {GetName} from "./MainPage";
import {useNavigate} from "react-router-dom";
import {PersonAddAlt} from "@mui/icons-material";
import {GetUserAPI} from "../../Services/UserAPI";




export function AdminPage() {
    const dispatch = useDispatch();
    const [data, setData] = useState<User[]>([]);
    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(10);
    const [totalCount, setTotalCount] = useState(0);

    const [loading, setLoading] = useState(false);

    const [searchText, setSearchText] = useState<string>("");

    const navigate = useNavigate();

    useEffect(() => {
        dispatch(setTitle(pageData.admin.pageTitle));
        document.title = pageData.admin.pageTitle;
        fetchData(page, rowsPerPage, searchText).catch(error => {
            console.error('Failed to fetch data:', error);
        });
        // eslint-disable-next-line
    }, [dispatch, page, rowsPerPage]);


    const fetchData = async (page: number, rowsPerPage: number, phoneOrMail = ""): Promise<void> => {
        setLoading(true);
        try {
            const response = await GetUserAPI(page, rowsPerPage, phoneOrMail);

            setData(response.data.users);
            setTotalCount(response.data.totalCount);
        } catch (error) {
            console.error('Failed to fetch data:', error);
        }
        setLoading(false);
    };

    const handleChangePage = (_event: React.ChangeEvent<unknown>, newPage: number): void => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: SelectChangeEvent<number>): void => {
        setRowsPerPage(parseInt(event.target.value as string, 10));
        setPage(1); // Reset to first page with new page size
    };

    function handleSearchOnclick() {
        fetchData(page, rowsPerPage, searchText).catch(error => {
            console.error('Failed to fetch data:', error);
        });
    }

    const handleAddUserOnclick = () => {
        navigate("/admin/0")
    };
    return (
        <Paper sx={{margin: '10px'}}>
            <Grid container={true}>
                <Grid container={true} item={true} xs={12}>
                    <Grid xs item={true}>
                        <FormControl size={"small"} sx={{m: 1, minWidth: 120}}>
                            <InputLabel id="rows-per-page-select-label">Rows</InputLabel>
                            <Select
                                labelId="rows-per-page-select-label"
                                id="rows-per-page-select"
                                value={rowsPerPage}
                                label="Rows"
                                onChange={handleChangeRowsPerPage}
                            >
                                {[5, 10, 15, 20].map((pageSize) => (
                                    <MenuItem key={pageSize} value={pageSize}>
                                        {pageSize}
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid columnSpacing={1} justifyItems={"center"} alignContent={"center"} container={true} item={true} xs={5}>
                        <Grid item={true} xs={5}>
                            <TextField value={searchText} fullWidth={true} size={"small"} id="outlined-basic"
                                       onChange={(event) => setSearchText(event.target.value)}
                                       label="ค้นหา" variant="outlined"/>
                        </Grid>
                        <Grid item={true} xs={3}>
                            <Button
                                onClick={handleSearchOnclick}
                                fullWidth={true}
                                variant={"outlined"} sx={{
                                    backgroundColor: '#2E3192',
                                    color: '#FFFF',
                                    '&:hover': {backgroundColor: '#504DC2'}
                                }}
                            >
                                ค้นหา
                            </Button>
                        </Grid>
                        <Grid item={true} xs={4}>
                            <Button
                                onClick={handleAddUserOnclick}
                                fullWidth={true}
                                variant="outlined"
                                sx={{
                                    backgroundColor: '#4DBCFA',
                                    color: 'white',
                                    '&:hover': {
                                        backgroundColor: '#3BACD4',
                                    },
                                }}
                                startIcon={<PersonAddAlt />}
                            >
                                เพิ่มบัญชีผู้ใช้
                            </Button>
                        </Grid>

                    </Grid>
                </Grid>
                <Grid item={true} xs={12}>
                    <TableContainer component={Paper}>
                        <Table sx={{minWidth: 650}} aria-label="simple table">
                            <TableHead>
                                <TableRow>
                                    <TableCell sx={{backgroundColor: "#4B4B4B", color: "white"}}
                                               align={"center"}>ลำดับ</TableCell>
                                    <TableCell sx={{backgroundColor: "#4B4B4B", color: "white"}}
                                               align={"left"}>ชื่อ-นามสกุล</TableCell>
                                    <TableCell sx={{backgroundColor: "#4B4B4B", color: "white"}}
                                               align="left">ชื่อผู้ใช้</TableCell>
                                    <TableCell sx={{backgroundColor: "#4B4B4B", color: "white"}}
                                               align="left">เบอร์</TableCell>
                                    <TableCell sx={{backgroundColor: "#4B4B4B", color: "white"}}
                                               align="left">อีเมล</TableCell>
                                    <TableCell sx={{backgroundColor: "#4B4B4B", color: "white"}}
                                               align="left">ตำแหน่ง</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {loading ?
                                    // This will render a skeleton table row 5 times
                                    Array(5).fill(0).map((_, index) => (
                                        <TableRow key={index}>
                                            <TableCell>
                                                <Skeleton/>
                                            </TableCell>
                                            <TableCell>
                                                <Skeleton/>
                                            </TableCell>
                                            <TableCell>
                                                <Skeleton/>
                                            </TableCell>
                                            <TableCell>
                                                <Skeleton/>
                                            </TableCell>
                                            <TableCell>
                                                <Skeleton/>
                                            </TableCell>
                                            <TableCell>
                                                <Skeleton/>
                                            </TableCell>
                                        </TableRow>
                                    )) :
                                    data.map((user: User, index) => (
                                        <TableRow
                                            key={user.id}
                                            sx={{'&:last-child td, &:last-child th': {border: 0}}}
                                        >
                                            <TableCell align={"center"}>
                                                {(page - 1) * rowsPerPage + index + 1}
                                            </TableCell>
                                            <TableCell align={"left"} component="th" scope="row">
                                                {GetName(user)}
                                            </TableCell>
                                            <TableCell align="left">{user.userName}</TableCell>
                                            <TableCell align="left">{user.phoneNumber}</TableCell>
                                            <TableCell align="left">{user.email}</TableCell>
                                            <TableCell align="left">{user.roles[0].roleName}</TableCell>
                                        </TableRow>
                                    ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Grid>
                <Grid xs={12} item={true} container={true} justifyContent="flex-end">
                    <Pagination
                        count={Math.ceil(totalCount / rowsPerPage)}
                        page={page}
                        onChange={handleChangePage}
                        color="primary"
                        sx={{my: 2}}
                    />
                </Grid>
            </Grid>


        </Paper>
    );
}
