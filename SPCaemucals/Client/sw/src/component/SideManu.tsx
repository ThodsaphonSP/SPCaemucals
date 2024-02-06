import React, {useState} from 'react';
import { Drawer, List, ListItemIcon, ListItemText, ListItemButton, Collapse } from '@mui/material';
import {
    Circle,
    ExpandMore,
    ExpandLess,
    HomeOutlined,
    Inventory2Outlined,
    RecentActors,
    Person,
    PlaylistAdd, Home
} from "@mui/icons-material";
import FactoryIcon from '@mui/icons-material/Factory';



import { Link } from "react-router-dom";
import Box from "@mui/material/Box";
import GroupIcon from '@mui/icons-material/Group';



const drawerWidth = 240;



export function SideManu() {


    type system = "admin" | "contact" | "account" | "good" | "warehouse"|"task"|"Person"|"FirstPage"|"manufacture";

    const [firstPageOpen, setFirstPageOpen] = React.useState(false);
    const [adminOpen, setAdminOpen] = React.useState(false);
    const [wareHouseOpen, setWareHouseOpen] = React.useState<boolean>(false);
    const [manufactureOpen, setManufactureOpen] = React.useState<boolean>(false);

    const [taskOpen,setTaskOpen] = useState<boolean>(false);

    const [accountOpen, setAccountOpen] = useState(false);

    const [personOpen, setPersonOpen] = React.useState(false);
    const [goodsOpen, setGoodsOpen] = React.useState(false);

    const [contactOpen, setContactOpen] = React.useState(false);

    const setAll = [setAdminOpen, setAccountOpen, setGoodsOpen, setContactOpen,setWareHouseOpen,setTaskOpen,setPersonOpen,setManufactureOpen,setFirstPageOpen];

    function closeAllExceptOne(except: React.Dispatch<React.SetStateAction<boolean>>) {
        const closeAllExceptOneSet = setAll.filter(x => x !== except);

        closeAllExceptOneSet.forEach(x => x(false));
    }
    const handleOpen = (value: system) => {
        switch (value) {

            case "manufacture":
                setManufactureOpen(!manufactureOpen);
                if (!manufactureOpen) {
                    closeAllExceptOne(setManufactureOpen)
                }
                break;

            case "FirstPage":
                setFirstPageOpen(!firstPageOpen);
                if (!firstPageOpen) {
                    closeAllExceptOne(setFirstPageOpen)
                }
                break;

            case "admin":
                setAdminOpen(!adminOpen);
                if (!adminOpen) {
                    closeAllExceptOne(setAdminOpen)
                }
                break;
            case "account":
                setAccountOpen(!accountOpen);
                if (!accountOpen) {
                    closeAllExceptOne(setAccountOpen)
                }

                break;

                case "Person":
                setPersonOpen(!personOpen);
                if (!personOpen) {
                    closeAllExceptOne(setPersonOpen)
                }
                break;

            case "good":
                setGoodsOpen(!goodsOpen);
                if (!goodsOpen) {
                    closeAllExceptOne(setGoodsOpen);
                }
                break;
            case "warehouse":
                setWareHouseOpen(!wareHouseOpen);
                if (!wareHouseOpen) {
                    closeAllExceptOne(setWareHouseOpen);
                }
                break;
            case "contact":
                setContactOpen(!contactOpen);
                if (!contactOpen) {
                    closeAllExceptOne(setContactOpen)
                }
                break;
            case "task":
                setTaskOpen(!taskOpen);
                if (!taskOpen) {
                    closeAllExceptOne(setTaskOpen)
                }
                break;
        }
    };



    const styles = {
        drawer: {
            width: drawerWidth,
            flexShrink: 0,
            '& .MuiDrawer-paper': {
                width: drawerWidth,
                boxSizing: 'border-box',
                backgroundColor: '#2E3192', // Sets the background color to black
                color: 'white', // Sets the text color to white
                position: 'relative',
            },
        },
    };

    return (
        <Drawer
            sx={{ ...styles.drawer, height: '100%' }}
            variant="permanent"
            anchor="left"
        >

            <List>

            <Box sx={{ backgroundColor: firstPageOpen ? "orange" : "initial", borderTopRightRadius: '40px' }}>
                    <ListItemButton onClick={() => { handleOpen("FirstPage") }}>
                        <ListItemIcon>

                            <Home style={{color:"white"}} />
                        </ListItemIcon>
                        <Link to="/FirstPage" style={{textDecoration: 'none', color: 'inherit'}}>
                                    <ListItemText primary="หน้าแรก" />
                                </Link>
                    </ListItemButton>
                    <Collapse in={firstPageOpen} timeout="auto" unmountOnExit>
                    </Collapse>
                </Box>

                <Box sx={{ backgroundColor: adminOpen ? "orange" : "initial", borderTopRightRadius: '40px' }}>
                    <ListItemButton onClick={() => { handleOpen("admin") }}>
                        <ListItemIcon>
                            <GroupIcon style={{ color: 'white' }} />
                        </ListItemIcon>
                        <ListItemText primary="ระบบแอดมิน" />
                        {adminOpen ? <ExpandLess /> : <ExpandMore />}
                    </ListItemButton>
                    <Collapse in={adminOpen} timeout="auto" unmountOnExit>
                        <List component="div" disablePadding>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/admin" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="บัญชีผู้ใช้งาน" />
                                </Link>
                            </ListItemButton>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/Authority" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="จัดการสิทธิ์" />
                                </Link>
                            </ListItemButton>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                  <Link to="/User" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="จัดการผู้ใช้" />
                                </Link>
                            </ListItemButton>
                        </List>
                    </Collapse>
                </Box>


                <Box sx={{ backgroundColor: contactOpen ? "orange" : "initial", borderTopRightRadius: '40px' }}>
                    <ListItemButton onClick={() => { handleOpen("contact") }}>
                        <ListItemIcon>

                            <RecentActors style={{ color: 'white' }}/>
                        </ListItemIcon>
                        <ListItemText primary="ระบบรายชื่อ" />
                        {contactOpen ? <ExpandLess /> : <ExpandMore />}
                    </ListItemButton>
                    <Collapse in={contactOpen} timeout="auto" unmountOnExit>
                        <List component="div" disablePadding>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/all-contacts" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="รายชื่อทั้งหมด" />
                                </Link>
                            </ListItemButton>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/employee" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="พนักงาน" />
                                </Link>
                            </ListItemButton>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/customer" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="ลูกค้า-คู่ค้า" />
                                </Link>
                            </ListItemButton>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/raw-material" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="สารวัตถุดิบ" />
                                </Link>
                            </ListItemButton>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/packaging" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="บรรจุภัณฑ์" />
                                </Link>
                            </ListItemButton>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/machine" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="เครื่องจักร" />
                                </Link>
                            </ListItemButton>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/another" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="ประเภทอื่นๆ" />
                                </Link>
                            </ListItemButton>
                        </List>


                    </Collapse>


                </Box>




                <Box sx={{ backgroundColor: personOpen ? "orange" : "initial", borderTopRightRadius: '40px' }}>
                    <ListItemButton onClick={() => { handleOpen("Person") }}>
                        <ListItemIcon>

                            <Person style={{ color: 'white' }}/>
                        </ListItemIcon>
                        <ListItemText primary="ระบบบุคคล" />
                        {personOpen ? <ExpandLess /> : <ExpandMore />}
                    </ListItemButton>
                    <Collapse in={personOpen} timeout="auto" unmountOnExit>
                        <List component="div" disablePadding>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/Person" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="บริษัท เลเจนด์ อ๊อฟ โปรดักส์ จำกัด" />
                                </Link>
                            </ListItemButton>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/hr-cosmic" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="บริษัท อินคอสแม็ก จำกัด" />
                                </Link>
                            </ListItemButton>
                        </List>
                    </Collapse>
                </Box>



                <Box sx={{ backgroundColor: goodsOpen ? "orange" : "initial", borderTopRightRadius: '40px' }}>
                    <ListItemButton onClick={() => { handleOpen("good") }}>
                        <ListItemIcon>
                            <FactoryIcon style={{color:"white"}} />
                        </ListItemIcon>
                        <ListItemText primary="ระบบสร้างสินค้า" />
                        {goodsOpen ? <ExpandLess /> : <ExpandMore />}
                    </ListItemButton>
                    <Collapse in={goodsOpen} timeout="auto" unmountOnExit>
                        <List component="div" disablePadding>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/all-goods" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="กลุ่มทั้งหมด" />
                                </Link>
                            </ListItemButton>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/cosmetic" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="Cosmetic" />
                                </Link>
                            </ListItemButton>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/food" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="Food-Drink" />
                                </Link>
                            </ListItemButton>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/create-raw-material" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="Raw Material" />
                                </Link>
                            </ListItemButton>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/pet-shop" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="Pet Shop" />
                                </Link>
                            </ListItemButton>
                        </List>


                    </Collapse>


                </Box>

                <Box sx={{ backgroundColor: manufactureOpen ? "orange" : "initial", borderTopRightRadius: '40px' }}>
                    <ListItemButton onClick={() => handleOpen("manufacture")}>
                        <ListItemIcon>
                            <Inventory2Outlined style={{ color: 'white' }} />
                        </ListItemIcon>
                        <ListItemText primary="ระบบผลิต" />
                        {manufactureOpen ? <ExpandLess /> : <ExpandMore />}
                    </ListItemButton>
                    <Collapse in={manufactureOpen} timeout="auto" unmountOnExit>
                        <List component="div" disablePadding>
                            <Link to="/all-manufacture" style={{ textDecoration: 'none', color: 'inherit' }}>
                                <ListItemButton sx={{ pl: 4 }}>
                                    <ListItemIcon>
                                        <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                    </ListItemIcon>
                                    <ListItemText primary="การสั่งซื้อ" />
                                </ListItemButton>
                            </Link>
                        </List>
                    </Collapse>
                    <Collapse in={manufactureOpen} timeout="auto" unmountOnExit>
                        <List component="div" disablePadding>
                            <Link to="/all-rawmaterial" style={{ textDecoration: 'none', color: 'inherit' }}>
                                <ListItemButton sx={{ pl: 4 }}>
                                    <ListItemIcon>
                                        <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                    </ListItemIcon>
                                    <ListItemText primary="สารวัตถุดิบ" />
                                </ListItemButton>
                            </Link>
                        </List>
                    </Collapse>
                    <Collapse in={manufactureOpen} timeout="auto" unmountOnExit>
                        <List component="div" disablePadding>
                            <Link to="/all-productformula" style={{ textDecoration: 'none', color: 'inherit' }}>
                                <ListItemButton sx={{ pl: 4 }}>
                                    <ListItemIcon>
                                        <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                    </ListItemIcon>
                                    <ListItemText primary="สูตรสินค้า" />
                                </ListItemButton>
                            </Link>
                        </List>
                    </Collapse>
                    <Collapse in={manufactureOpen} timeout="auto" unmountOnExit>
                        <List component="div" disablePadding>
                            <Link to="/all-testanddev" style={{ textDecoration: 'none', color: 'inherit' }}>
                                <ListItemButton sx={{ pl: 4 }}>
                                    <ListItemIcon>
                                        <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                    </ListItemIcon>
                                    <ListItemText primary="Test & Dev" />
                                </ListItemButton>
                            </Link>
                        </List>
                    </Collapse>

                    
                </Box>

                <Box sx={{ backgroundColor: wareHouseOpen ? "orange" : "initial", borderTopRightRadius: '40px' }}>
                    <ListItemButton onClick={() => handleOpen("warehouse")}>
                        <ListItemIcon>
                            <HomeOutlined style={{ color: 'white' }} />
                        </ListItemIcon>
                        <ListItemText primary="ระบบคลังสินค้า" />
                        {wareHouseOpen ? <ExpandLess /> : <ExpandMore />}
                    </ListItemButton>
                    <Collapse in={wareHouseOpen} timeout="auto" unmountOnExit>
                        <List component="div" disablePadding>
                            <Link to="/all-warehouse" style={{ textDecoration: 'none', color: 'inherit' }}>
                                <ListItemButton sx={{ pl: 4 }}>
                                    <ListItemIcon>
                                        <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                    </ListItemIcon>
                                    <ListItemText primary="การสั่งซื้อ" />
                                </ListItemButton>
                            </Link>
                        </List>
                        <List component="div" disablePadding>
                            <Link to="/dashboard-warehouse" style={{ textDecoration: 'none', color: 'inherit' }}>
                                <ListItemButton sx={{ pl: 4 }}>
                                    <ListItemIcon>
                                        <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                    </ListItemIcon>
                                    <ListItemText primary="คลังของทั้งหมด" />
                                </ListItemButton>
                            </Link>
                        </List>
                    </Collapse>
                    
                </Box>

                <Box sx={{ backgroundColor: taskOpen ? "orange" : "initial", borderTopRightRadius: '40px' }}>
                    <ListItemButton onClick={() => { handleOpen("task") }}>
                        <ListItemIcon>
                            <PlaylistAdd style={{color: "white"}} />
                        </ListItemIcon>
                        <ListItemText primary="ระบบสั่งงาน" />
                        {taskOpen ? <ExpandLess /> : <ExpandMore />}
                    </ListItemButton>
                    <Collapse in={taskOpen} timeout="auto" unmountOnExit>
                        <List component="div" disablePadding>
                            <ListItemButton sx={{ pl: 4 }}>
                                <ListItemIcon>
                                    <Circle sx={{ fontSize: 10 }} style={{ color: 'white' }} />
                                </ListItemIcon>
                                <Link to="/tasklist" style={{ textDecoration: 'none', color: 'inherit' }}>
                                    <ListItemText primary="รายการสั่งงาน" />
                                </Link>
                            </ListItemButton>
                        </List>


                    </Collapse>


                </Box>

            </List>

        </Drawer>
    );
}
