import CachedIcon from '@mui/icons-material/Cached';
import CloseIcon from '@mui/icons-material/Close';
import Box from '@mui/material/Box';
import Dialog from '@mui/material/Dialog';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import IconButton from '@mui/material/IconButton';
import Snackbar from '@mui/material/Snackbar';
import Tooltip from '@mui/material/Tooltip';
import Typography, { TypographyProps } from '@mui/material/Typography';
import * as React from 'react';

import {
    addBotsToWorker,
    deleteBotsFromWorker,
    getWorker,
    startBotInWorker,
    stopBotInWorker
} from '../../api/workers';
import { Bot, Guid, SuccessResponse, Worker } from '../../utils/types';
import SimpleTable from '../tables/SimpleTable';

function getExcludedBots(bots: Bot[], worker: Worker): Bot[] {
    return bots.filter(
        (bot: Bot) => !worker.bots.some((rb: Bot) => rb.id === bot.id)
    ) as Bot[];
}

function getBotName(bots: Bot[], botId: Guid): string {
    return bots.find((bot: Bot) => bot.id === botId)?.name ?? '';
}

export default function EditWorkerModal({
    open,
    onClose,
    worker,
    bots
}: {
    open: boolean;
    onClose: () => void;
    worker: Worker;
    bots: Bot[];
}) {
    const [currentWorker, setCurrentWorker] = React.useState<
        Worker | undefined
    >({
        ...worker
    });
    const [currentBots, setCurrentBots] = React.useState<Bot[]>(
        getExcludedBots(bots, worker)
    );
    const [snack, setSnack] = React.useState<{
        open: boolean;
        message: string;
    }>({
        open: false,
        message: ''
    });

    if (!currentWorker) {
        return null;
    }

    const resetData = () => {
        getWorker(worker.id).then((w: Worker) => {
            setCurrentWorker({
                ...w
            });
            setCurrentBots(getExcludedBots(bots, w));
        });
    };

    const handleStart = (botId: Guid) => {
        startBotInWorker(currentWorker.id, botId)
            .then((res: SuccessResponse) => {
                if (res.success) {
                    setSnack({ open: true, message: 'Bot started.' });
                    return;
                }

                setSnack({
                    open: true,
                    message: 'An error ocurred when trying to start the bot.'
                });
            })
            .catch(() => {
                setSnack({
                    open: true,
                    message: 'An error ocurred when trying to start the bot.'
                });
            });
    };

    const handleStop = (botId: Guid) => {
        stopBotInWorker(currentWorker.id, botId)
            .then((res: SuccessResponse) => {
                if (res.success) {
                    setSnack({ open: true, message: 'Bot stopped.' });
                    return;
                }

                setSnack({
                    open: true,
                    message: 'An error ocurred when trying to stop the bot.'
                });
            })
            .catch(() => {
                setSnack({
                    open: true,
                    message: 'An error ocurred when trying to stop the bot.'
                });
            });
    };

    const handleAdd = (botId: Guid) => {
        addBotsToWorker(currentWorker.id, [botId])
            .then((res: SuccessResponse) => {
                if (res.success) {
                    setSnack({
                        open: true,
                        message: 'The bot was added to the worker.'
                    });
                    resetData();
                    return;
                }

                setSnack({
                    open: true,
                    message:
                        'An error ocurred when trying to add the bot to the worker.'
                });
            })
            .catch(() => {
                setSnack({
                    open: true,
                    message:
                        'An error ocurred when trying to add the bot to the worker.'
                });
            });
    };

    const handleRemove = (botId: Guid) => {
        deleteBotsFromWorker(currentWorker.id, [botId])
            .then((res: SuccessResponse) => {
                if (res.success) {
                    setSnack({
                        open: true,
                        message: 'The bot was removed from the worker.'
                    });
                    resetData();
                    return;
                }

                setSnack({
                    open: true,
                    message:
                        'An error ocurred when trying to remove the bot from the worker.'
                });
            })
            .catch(() => {
                setSnack({
                    open: true,
                    message:
                        'An error ocurred when trying to remove the bot from the worker.'
                });
            });
    };

    const handleClose = () => {
        setCurrentWorker(undefined);
        setCurrentBots([]);

        onClose();
    };

    return (
        <Dialog open={open} onClose={handleClose} maxWidth="md" fullWidth>
            <DialogTitle
                sx={{ display: 'flex', justifyContent: 'space-between' }}
            >
                <Typography variant={'string' as TypographyProps['variant']}>
                    Edit worker ({currentWorker.id})
                </Typography>

                <Box>
                    <Tooltip title="Sync" placement="top">
                        <IconButton onClick={resetData} size="large">
                            <CachedIcon />
                        </IconButton>
                    </Tooltip>
                    <IconButton onClick={handleClose}>
                        <CloseIcon />
                    </IconButton>
                </Box>
            </DialogTitle>
            <DialogContent>
                <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
                    <SimpleTable
                        headers={[
                            { id: 'running', displayName: 'Status' },
                            { id: 'name', displayName: 'Name' },
                            {
                                id: 'prefix',
                                displayName: 'Prefix',
                                align: 'center'
                            },
                            {
                                id: 'userCommands',
                                displayName: 'Commands',
                                align: 'center'
                            }
                        ]}
                        rows={currentWorker.bots.map(bot => {
                            return {
                                ...bot,
                                name: getBotName(bots, bot.id)
                            };
                        })}
                        actions={[
                            {
                                id: 'start',
                                icon: 'play_circle',
                                iconColor: '#44b033',
                                handleClick: handleStart,
                                tooltip: 'Start bot',
                                tooltipDirection: 'left'
                            },
                            {
                                id: 'stop',
                                icon: 'stop_circle',
                                iconColor: '#b02537',
                                handleClick: handleStop,
                                tooltip: 'Stop bot',
                                tooltipDirection: 'right'
                            },
                            {
                                id: 'remove',
                                icon: 'delete',
                                handleClick: handleRemove,
                                tooltip: 'Remove bot from this worker',
                                tooltipDirection: 'right'
                            }
                        ]}
                    />
                    <SimpleTable
                        headers={[
                            { id: 'name', displayName: 'Name' },
                            {
                                id: 'prefix',
                                displayName: 'Prefix',
                                align: 'center'
                            },
                            {
                                id: 'userCommands',
                                displayName: 'Commands',
                                align: 'center'
                            }
                        ]}
                        rows={currentBots}
                        actions={[
                            {
                                id: 'add',
                                icon: 'add',
                                handleClick: handleAdd,
                                tooltip: 'Add bot to this worker',
                                tooltipDirection: 'right'
                            }
                        ]}
                    />
                </Box>
            </DialogContent>

            <Snackbar
                open={snack.open}
                autoHideDuration={3000}
                onClose={() => setSnack({ ...snack, open: false })}
                message={snack.message}
            />
        </Dialog>
    );
}
