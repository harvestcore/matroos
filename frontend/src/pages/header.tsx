import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Toolbar from '@mui/material/Toolbar';

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

export default function HeaderBar() {
    return (
        <AppBar position="fixed">
            <Toolbar disableGutters>
                <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
                    {pages.map(page => (
                        <Button
                            key={page.href}
                            size={page.isHome ? 'large' : 'small'}
                            sx={{ color: 'white', display: 'block' }}
                        >
                            {page.title}
                        </Button>
                    ))}
                </Box>
            </Toolbar>
        </AppBar>
    );
}
