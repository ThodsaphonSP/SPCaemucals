import {Box, Paper} from "@mui/material";
import {BaseContainerProps} from "./AdminEditPage";

export function BaseContainer(props: BaseContainerProps) {
    return <Paper sx={{margin: "10px"}}>
        <Box sx={{padding:"20px"}}>{props.children}</Box>
    </Paper>;
}