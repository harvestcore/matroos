import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Collapse from '@mui/material/Collapse';
import IconButton, { IconButtonProps } from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import { styled } from '@mui/material/styles';
import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

import styles from '../Home.module.css';
import { getAllBots } from '../api/bots';
import { getAllUserCommands } from '../api/commands';
import { getAllWorkers } from '../api/workers';
import {
    Bot,
    CommandType,
    ItemsResponse,
    UserCommand,
    Worker
} from '../utils/types';

export type CommandCardType = {
    label: string;
    count: number;
};

export type CommandCard = {
    count: number;
    commands: CommandCardType[];
};

export const getStaticProps = async () => {
    const bots: ItemsResponse<Bot> = await getAllBots();
    const userCommands: ItemsResponse<UserCommand> = await getAllUserCommands();
    const workers: ItemsResponse<Worker> = await getAllWorkers();

    const botsCard = {
        count: bots.count,
        commandsConfigured: bots.items.reduce(
            (acc, bot) => acc + bot.userCommands.length,
            0
        )
    };

    const commandsCard = {
        count: userCommands.count,
        commands: [
            {
                label: 'Message',
                count: userCommands.items.filter(
                    command => command.type === CommandType.MESSAGE
                ).length
            },
            {
                label: 'Ping',
                count: userCommands.items.filter(
                    command => command.type === CommandType.PING
                ).length
            },
            {
                label: 'Status',
                count: userCommands.items.filter(
                    command => command.type === CommandType.STATUS
                ).length
            },
            {
                label: 'Timer',
                count: userCommands.items.filter(
                    command => command.type === CommandType.TIMER
                ).length
            },
            {
                label: 'Version',
                count: userCommands.items.filter(
                    command => command.type === CommandType.VERSION
                ).length
            }
        ]
    };

    const workersCard = {
        count: workers.count,
        running: workers.items.reduce(
            (acc, worker) =>
                acc +
                worker.bots.reduce(
                    (_acc, _bot) => _acc + (_bot.running ? 1 : 0),
                    0
                ),
            0
        ),
        botsConfigured: workers.items.reduce(
            (acc, worker) => acc + worker.bots.length,
            0
        )
    };

    return {
        botsCard,
        commandsCard,
        workersCard
    };
};

interface ExpandMoreProps extends IconButtonProps {
    expand: boolean;
}

const ExpandMore = styled((props: ExpandMoreProps) => {
    const { expand, ...other } = props;
    return <IconButton {...other} />;
})(({ theme, expand }) => ({
    transform: !expand ? 'rotate(0deg)' : 'rotate(180deg)',
    marginLeft: 'auto',
    transition: theme.transitions.create('transform', {
        duration: theme.transitions.duration.shortest
    })
}));

export default function Home() {
    const [expanded, setExpanded] = useState(false);
    const [botsCard, setBotsCard] = useState({
        count: 0,
        commandsConfigured: 0
    });
    const [commandsCard, setCommandsCard] = useState({
        count: 0,
        commands: []
    } as CommandCard);
    const [workersCard, setWorkersCard] = useState({
        count: 0,
        running: 0,
        botsConfigured: 0
    });
    //const history = useHistory();

    const handleExpandClick = () => {
        setExpanded(!expanded);
    };

    useEffect(() => {
        const getData = async () => {
            const bots: ItemsResponse<Bot> = await getAllBots();
            const userCommands: ItemsResponse<UserCommand> =
                await getAllUserCommands();
            const workers: ItemsResponse<Worker> = await getAllWorkers();

            const botsCard = {
                count: bots.count,
                commandsConfigured: bots.items.reduce(
                    (acc, bot) => acc + bot.userCommands.length,
                    0
                )
            };

            const commandsCard = {
                count: userCommands.count,
                commands: [
                    {
                        label: 'Message',
                        count: userCommands.items.filter(
                            command => command.type === CommandType.MESSAGE
                        ).length
                    },
                    {
                        label: 'Ping',
                        count: userCommands.items.filter(
                            command => command.type === CommandType.PING
                        ).length
                    },
                    {
                        label: 'Status',
                        count: userCommands.items.filter(
                            command => command.type === CommandType.STATUS
                        ).length
                    },
                    {
                        label: 'Timer',
                        count: userCommands.items.filter(
                            command => command.type === CommandType.TIMER
                        ).length
                    },
                    {
                        label: 'Version',
                        count: userCommands.items.filter(
                            command => command.type === CommandType.VERSION
                        ).length
                    }
                ]
            };

            const workersCard = {
                count: workers.count,
                running: workers.items.reduce(
                    (acc, worker) =>
                        acc +
                        worker.bots.reduce(
                            (_acc, _bot) => _acc + (_bot.running ? 1 : 0),
                            0
                        ),
                    0
                ),
                botsConfigured: workers.items.reduce(
                    (acc, worker) => acc + worker.bots.length,
                    0
                )
            };

            setBotsCard(botsCard);
            setCommandsCard(commandsCard);
            setWorkersCard(workersCard);
        };

        getData();
    }, []);

    return (
        <div className={styles.container}>
            <main className={styles.main}>
                <h1 className={styles.title}>Matroos&apos; status</h1>

                <Box
                    sx={{
                        width: '100%',
                        display: 'flex',
                        flexDirection: 'row',
                        justifyContent: 'space-around',
                        marginY: '3em'
                    }}
                >
                    <Box sx={{ width: '30%' }}>
                        <Card variant="outlined">
                            <CardContent>
                                <Typography
                                    sx={{ mb: 1.5 }}
                                    variant="h5"
                                    component="div"
                                >
                                    Bots
                                </Typography>
                                <Typography color="text.secondary">
                                    Total: {botsCard.count}
                                </Typography>
                                <Typography color="text.secondary">
                                    Commands configured:{' '}
                                    {botsCard.commandsConfigured}
                                </Typography>
                            </CardContent>
                            <CardActions>
                                <Button size="small">
                                    <Link
                                        to="/bots"
                                        style={{
                                            textDecoration: 'none',
                                            color: 'inherit'
                                        }}
                                    >
                                        More
                                    </Link>
                                </Button>
                            </CardActions>
                        </Card>
                    </Box>
                    <Box sx={{ width: '30%' }}>
                        <Card variant="outlined">
                            <CardContent>
                                <Typography
                                    sx={{ mb: 1.5 }}
                                    variant="h5"
                                    component="div"
                                >
                                    Commands
                                </Typography>
                                <Typography color="text.secondary">
                                    Total: {commandsCard.count}
                                </Typography>
                            </CardContent>
                            <CardActions
                                sx={{
                                    width: '100%',
                                    display: 'flex',
                                    flexDirection: 'row',
                                    justifyContent: 'space-between'
                                }}
                            >
                                <Button size="small">
                                    <Link
                                        to="/commands"
                                        style={{
                                            textDecoration: 'none',
                                            color: 'inherit'
                                        }}
                                    >
                                        More
                                    </Link>
                                </Button>

                                <ExpandMore
                                    expand={expanded}
                                    onClick={handleExpandClick}
                                    aria-expanded={expanded}
                                    sx={{
                                        marginX: '0.5em'
                                    }}
                                >
                                    <ExpandMoreIcon />
                                </ExpandMore>
                            </CardActions>
                            <Collapse
                                in={expanded}
                                timeout="auto"
                                unmountOnExit
                            >
                                <CardContent>
                                    {commandsCard.commands.map(
                                        (
                                            {
                                                label,
                                                count
                                            }: { label: string; count: number },
                                            index: number
                                        ) => (
                                            <Typography
                                                key={index}
                                                color="text.secondary"
                                            >
                                                {label}: {count}
                                            </Typography>
                                        )
                                    )}
                                </CardContent>
                            </Collapse>
                        </Card>
                    </Box>
                    <Box sx={{ width: '30%' }}>
                        <Card variant="outlined">
                            <CardContent>
                                <Typography
                                    sx={{ mb: 1.5 }}
                                    variant="h5"
                                    component="div"
                                >
                                    Workers
                                </Typography>
                                <Typography color="text.secondary">
                                    Total: {workersCard.count}
                                </Typography>
                                <Typography color="text.secondary">
                                    Bots configured:{' '}
                                    {workersCard.botsConfigured}
                                </Typography>
                                <Typography color="text.secondary">
                                    Bots running: {workersCard.running}
                                </Typography>
                            </CardContent>
                            <CardActions>
                                <Button size="small">
                                    <Link
                                        to="/workers"
                                        style={{
                                            textDecoration: 'none',
                                            color: 'inherit'
                                        }}
                                    >
                                        More
                                    </Link>
                                </Button>
                            </CardActions>
                        </Card>
                    </Box>
                </Box>
            </main>
        </div>
    );
}
