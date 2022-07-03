import CloseIcon from '@mui/icons-material/Close';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import IconButton from '@mui/material/IconButton';
import Snackbar from '@mui/material/Snackbar';
import TextField from '@mui/material/TextField';
import * as React from 'react';

import { createBot } from '../../api/bots';
import { Bot, NullGuid, SuccessResponse, UserCommand } from '../../utils/types';
import ChipSelector from '../selectors/ChipSelector';

function getEmptyBot() {
    return {
        id: NullGuid,
        name: '',
        description: '',
        key: '',
        prefix: '',
        running: false,
        userCommands: []
    };
}

export default function CreateBotModal({
    open,
    onClose,
    commands
}: {
    open: boolean;
    onClose: () => void;
    commands: UserCommand[];
}) {
    const [currentBot, setCurrentBot] = React.useState<Bot>(getEmptyBot());

    const [snack, setSnack] = React.useState<{
        open: boolean;
        message: string;
    }>({
        open: false,
        message: ''
    });

    const handleCommandsChange = (event: any) => {
        const {
            target: { value }
        } = event;

        setCurrentBot({ ...currentBot, userCommands: value });
    };

    const handleSaveClick = () => {
        const botCopy = { ...currentBot };
        botCopy.userCommands = botCopy.userCommands.map(
            command => (command as UserCommand).id
        );
        createBot(botCopy)
            .then((res: SuccessResponse) => {
                if (res.success) {
                    setSnack({ open: true, message: 'Bot created!' });
                    handleClose();
                }

                setSnack({
                    open: true,
                    message: 'An error ocurred when creating the bot.'
                });
            })
            .catch(() => {
                setSnack({
                    open: true,
                    message: 'An error ocurred when creating the bot.'
                });
            });
    };

    const handleChange = (event: any, field: string) => {
        const {
            target: { value }
        } = event;

        setCurrentBot({ ...currentBot, [field]: value });
    };

    const handleClose = () => {
        setCurrentBot(getEmptyBot());
        onClose();
    };

    const isButtonDisabled = () => {
        return (
            currentBot.name.trim() === '' ||
            currentBot.key.trim() === '' ||
            currentBot.prefix.trim() === ''
        );
    };

    return (
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle
                sx={{ display: 'flex', justifyContent: 'space-between' }}
            >
                Create bot
                <IconButton onClick={handleClose}>
                    <CloseIcon />
                </IconButton>
            </DialogTitle>
            <DialogContent>
                <Box
                    sx={{
                        display: 'flex',
                        flexDirection: 'column',
                        justifyContent: 'flex-start',
                        gap: 2,
                        marginTop: 1
                    }}
                    component="form"
                    autoComplete="off"
                >
                    <TextField
                        id="key"
                        label="Discord key"
                        variant="outlined"
                        value={currentBot.key}
                        required
                        onChange={(event: any) => handleChange(event, 'key')}
                    />
                    <Box
                        sx={{
                            display: 'flex',
                            justifyContent: 'space-around',
                            gap: 2
                        }}
                    >
                        <TextField
                            id="name"
                            label="Name"
                            sx={{ width: '75%' }}
                            variant="outlined"
                            value={currentBot.name}
                            required
                            onChange={(event: any) =>
                                handleChange(event, 'name')
                            }
                        />
                        <TextField
                            id="prefix"
                            label="Prefix"
                            sx={{ width: '25%' }}
                            variant="outlined"
                            value={currentBot.prefix}
                            required
                            onChange={(event: any) =>
                                handleChange(event, 'prefix')
                            }
                        />
                    </Box>
                    <TextField
                        id="description"
                        label="Description"
                        variant="outlined"
                        value={currentBot.description}
                        onChange={(event: any) =>
                            handleChange(event, 'description')
                        }
                    />

                    <ChipSelector
                        items={commands}
                        selectedItems={currentBot.userCommands}
                        idItemProperty="id"
                        displayNameItemProperty="name"
                        fieldInputId="userCommands"
                        fieldInputDisplayName="Commands"
                        handleChange={handleCommandsChange}
                    />
                </Box>
            </DialogContent>
            <DialogActions>
                <Box
                    sx={{
                        display: 'flex',
                        justifyContent: 'space-around',
                        gap: 2,
                        width: '100%',
                        marginX: 2,
                        marginBottom: 2
                    }}
                >
                    <Button
                        variant="outlined"
                        disabled={isButtonDisabled()}
                        onClick={handleSaveClick}
                        sx={{ width: '100%' }}
                    >
                        Create bot
                    </Button>
                </Box>
            </DialogActions>
            <Snackbar
                open={snack.open}
                autoHideDuration={3000}
                onClose={() => setSnack({ ...snack, open: false })}
                message={snack.message}
            />
        </Dialog>
    );
}
