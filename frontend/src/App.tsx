import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Toolbar from '@mui/material/Toolbar';
import { Link, Route, BrowserRouter as Router, Routes } from 'react-router-dom';

import Home from './pages';
import BotsHome from './pages/bots';
import CommandsHome from './pages/commands';
import WorkersHome from './pages/workers';

interface Page {
    title: string;
    href: string;
    isHome?: boolean;
}

const pages: Page[] = [
    {
        title: 'MATROOS',
        href: '/',
        isHome: true
    },
    {
        title: 'Bots',
        href: '/bots'
    },
    {
        title: 'Commands',
        href: '/commands'
    },
    {
        title: 'Workers',
        href: '/workers'
    }
];

export default function App() {
    const handleClick = (href: string) => {};

    return (
        <Router>
            <AppBar position="relative">
                <Toolbar disableGutters>
                    <Box
                        sx={{
                            flexGrow: 1,
                            display: { xs: 'none', md: 'flex' }
                        }}
                    >
                        {pages.map(page => (
                            <Button
                                key={page.href}
                                onClick={() => handleClick(page.href)}
                                size={page.isHome ? 'large' : 'small'}
                                sx={{ color: 'white', display: 'block' }}
                            >
                                <Link
                                    to={page.href}
                                    style={{
                                        textDecoration: 'none',
                                        color: 'white'
                                    }}
                                >
                                    {page.title}
                                </Link>
                            </Button>
                        ))}
                    </Box>
                </Toolbar>
            </AppBar>

            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/bots" element={<BotsHome />} />
                <Route path="/commands" element={<CommandsHome />} />
                <Route path="/workers" element={<WorkersHome />} />
            </Routes>
        </Router>
    );
}
