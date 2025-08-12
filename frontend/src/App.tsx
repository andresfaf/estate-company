import Navbar from './components/Navbar';
import { Container, Toolbar } from '@mui/material';
import AppRouter from './router/AppRouter';

function App() {
  return (
    <>
      <Navbar />
      <Toolbar />
      <Container>
        <AppRouter />
      </Container>
    </>
  )
}

export default App