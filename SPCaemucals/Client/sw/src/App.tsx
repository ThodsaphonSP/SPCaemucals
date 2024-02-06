
import './App.css';
import {createTheme, ThemeProvider} from "@mui/material";
import {MainPage} from "./page/admin/MainPage";


const theme = createTheme({
  typography: {
    fontFamily: [
      'Poppins',
      'sans-serif'
    ].join(','),
  },
  // other theme settings
});

function App() {
  return (<>
    <ThemeProvider theme={theme} >
      <MainPage></MainPage>
    </ThemeProvider >
  </>);
}

export default App;
