import {Paper} from "@mui/material";
import {BaseContainerProps} from "./AdminEditPage";

export function BaseContainer(props: BaseContainerProps) {
    return <Paper sx={{margin: "10px"}}>
        {props.children}
    </Paper>;
}