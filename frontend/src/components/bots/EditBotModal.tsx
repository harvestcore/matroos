import CloseIcon from '@mui/icons-material/Close';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
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

import { deleteBot, updateBot } from '../../api/bots';
import { Bot, SuccessResponse, UserCommand } from '../../utils/types';
import ConfirmationDialog from '../modals/confirmationDialog';
import ChipSelector from '../selectors/ChipSelector';

export default function EditBotModal({
    open,
    onClose,
    bot,
    commands
}: {
    open: boolean;
    onClose: () => void;
    bot: Bot;
    commands: UserCommand[];
}) {
    const filtered = commands.filter((c: UserCommand) =>
        (bot.userCommands as UserCommand[]).some(
            (bc: UserCommand) => bc.id === c.id
        )
    );
    const [currentBot, setCurrentBot] = React.useState<Bot>({
        ...bot,
        userCommands: filtered
    });
    const [confirmationIsOpen, setConfirmationIsOpen] =
        React.useState<boolean>(false);
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
        updateBot(botCopy)
            .then((res: SuccessResponse) => {
                if (res.success) {
                    setSnack({
                        open: true,
                        message: 'Bot configuration saved!'
                    });
                    return;
                }

                setSnack({
                    open: true,
                    message: 'An error ocurred when saving the configuration.'
                });
            })
            .catch(() => {
                setSnack({
                    open: true,
                    message: 'An error ocurred when saving the configuration.'
                });
            });
    };

    const handleRemoveClick = () => {
        setConfirmationIsOpen(true);
    };

    const handleConfirmationDialog = (response: boolean) => {
        if (response) {
            deleteBot(currentBot);
        }
        setConfirmationIsOpen(false);
        onClose();
    };

    const handleChange = (event: any, field: string) => {
        const {
            target: { value }
        } = event;

        setCurrentBot({ ...currentBot, [field]: value });
    };

    const isButtonDisabled = () => {
        return (
            currentBot.name.trim() === '' ||
            currentBot.key.trim() === '' ||
            currentBot.prefix.trim() === ''
        );
    };

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle
                sx={{ display: 'flex', justifyContent: 'space-between' }}
            >
                Edit bot
                <IconButton onClick={onClose}>
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
                        id="id"
                        label="Identifier"
                        variant="outlined"
                        InputProps={{
                            readOnly: true
                        }}
                        value={currentBot.id}
                    />
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
                        sx={{ width: '95%' }}
                    >
                        Save changes
                    </Button>
                    <Button
                        variant="outlined"
                        onClick={handleRemoveClick}
                        sx={{ width: '5%' }}
                        color="error"
                    >
                        <DeleteForeverIcon />
                    </Button>
                </Box>
            </DialogActions>
            <ConfirmationDialog
                open={confirmationIsOpen}
                handleClose={handleConfirmationDialog}
            />
            <Snackbar
                open={snack.open}
                autoHideDuration={3000}
                onClose={() => setSnack({ ...snack, open: false })}
                message={snack.message}
            />
        </Dialog>
    );
}
