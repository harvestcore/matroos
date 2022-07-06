import CloseIcon from '@mui/icons-material/Close';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import FormControl from '@mui/material/FormControl';
import IconButton from '@mui/material/IconButton';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import Select from '@mui/material/Select';
import Snackbar from '@mui/material/Snackbar';
import Switch from '@mui/material/Switch';
import TextField from '@mui/material/TextField';
import Tooltip from '@mui/material/Tooltip';
import Typography from '@mui/material/Typography';
import * as React from 'react';

import { deleteUserCommand, updateUserCommand } from '../../api/commands';
import ConfirmationDialog from '../../components/modals/confirmationDialog';
import { CommandModeToString } from '../../utils/commandMode';
import { CommandTypeToString } from '../../utils/commandType';
import {
    CommandMode,
    CommandSignature,
    CommandType,
    DataType,
    ParameterSignature,
    SuccessResponse,
    UserCommand
} from '../../utils/types';
import { areAllPropsProvided } from './utils';

export default function EditCommandModal({
    open,
    onClose,
    userCommand,
    commandSignatures,
    allUserCommands
}: {
    open: boolean;
    onClose: () => void;
    userCommand: UserCommand;
    commandSignatures: CommandSignature[];
    allUserCommands: UserCommand[];
}) {
    const [currentUserCommand, setCurrentUserCommand] =
        React.useState<UserCommand>({ ...userCommand });
    const [confirmationIsOpen, setConfirmationIsOpen] =
        React.useState<boolean>(false);
    const [snack, setSnack] = React.useState<{
        open: boolean;
        message: string;
    }>({
        open: false,
        message: ''
    });

    const handleChange = (
        event: any,
        field: string,
        isParam: boolean,
        isSwitch: boolean = false
    ) => {
        const {
            target: { value }
        } = event;

        if (field === 'type') {
            setCurrentUserCommand({
                ...currentUserCommand,
                parameters: {},
                mode: CommandMode.UNDEFINED,
                type: value as CommandType
            });

            return;
        }

        if (isParam) {
            setCurrentUserCommand({
                ...currentUserCommand,
                parameters: {
                    ...currentUserCommand.parameters,
                    [field]: isSwitch ? event.target.checked : value
                }
            });

            return;
        }

        setCurrentUserCommand({ ...currentUserCommand, [field]: value });
    };

    const handleSaveClick = () => {
        const newParameters = { ...currentUserCommand.parameters };

        if ('CommandId' in newParameters) {
            newParameters.CommandId = (
                newParameters.CommandId as UserCommand
            ).id;
        }

        updateUserCommand({
            ...currentUserCommand,
            parameters: { ...newParameters }
        })
            .then((res: SuccessResponse) => {
                if (res.success) {
                    setSnack({
                        open: true,
                        message: 'Command configuration saved!'
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
            deleteUserCommand(currentUserCommand);
            onClose();
        }
        setConfirmationIsOpen(false);
    };

    const isButtonDisabled = () => {
        return !areAllPropsProvided(currentUserCommand, commandSignatures);
    };

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle
                sx={{ display: 'flex', justifyContent: 'space-between' }}
            >
                Edit command
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
                        value={currentUserCommand.id}
                    />
                    <Box
                        sx={{
                            display: 'flex',
                            justifyContent: 'space-around',
                            gap: 2
                        }}
                    >
                        <TextField
                            id="type"
                            label="Type"
                            sx={{ width: '50%' }}
                            variant="outlined"
                            value={CommandTypeToString(currentUserCommand.type)}
                            InputProps={{
                                readOnly: true
                            }}
                            onChange={(event: any) =>
                                handleChange(event, 'type', false)
                            }
                        />
                        <TextField
                            id="mode"
                            label="Mode"
                            sx={{ width: '50%' }}
                            variant="outlined"
                            value={CommandModeToString(currentUserCommand.mode)}
                            InputProps={{
                                readOnly: true
                            }}
                            onChange={(event: any) =>
                                handleChange(event, 'mode', false)
                            }
                        />
                    </Box>
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
                            value={currentUserCommand.name}
                            required
                            onChange={(event: any) =>
                                handleChange(event, 'name', false)
                            }
                        />
                        <TextField
                            id="trigger"
                            label="Trigger"
                            sx={{ width: '25%' }}
                            variant="outlined"
                            value={currentUserCommand.trigger}
                            required
                            onChange={(event: any) =>
                                handleChange(event, 'trigger', false)
                            }
                        />
                    </Box>
                    <TextField
                        id="description"
                        label="Description"
                        variant="outlined"
                        value={currentUserCommand.description}
                        onChange={(event: any) =>
                            handleChange(event, 'description', false)
                        }
                    />

                    {currentUserCommand.type !== CommandType.UNDEFINED &&
                        (
                            commandSignatures.find(
                                commandSignature =>
                                    commandSignature.commandType ===
                                    currentUserCommand.type
                            )?.signature || []
                        ).map((ps: ParameterSignature, index: number) => {
                            return (
                                <Box
                                    key={index}
                                    sx={{
                                        display: 'flex',
                                        justifyContent: 'space-between',
                                        alignItems: 'center'
                                    }}
                                >
                                    <Typography>{ps.displayName}</Typography>
                                    {ps.dataType === DataType.BOOLEAN && (
                                        <Switch
                                            value={
                                                currentUserCommand.parameters[
                                                    ps.name
                                                ] || ps.default
                                            }
                                            required={ps.required}
                                            onChange={(event: any) =>
                                                handleChange(
                                                    event,
                                                    ps.name,
                                                    true,
                                                    true
                                                )
                                            }
                                        />
                                    )}
                                    {ps.dataType === DataType.DATE && (
                                        <Tooltip
                                            title="Insert a cron expression"
                                            placement="right"
                                        >
                                            <TextField
                                                sx={{ minWidth: '60%' }}
                                                label={ps.displayName}
                                                variant="outlined"
                                                value={
                                                    currentUserCommand
                                                        .parameters[ps.name] ||
                                                    ps.default
                                                }
                                                required={ps.required}
                                                onChange={(event: any) =>
                                                    handleChange(
                                                        event,
                                                        ps.name,
                                                        true
                                                    )
                                                }
                                            />
                                        </Tooltip>
                                    )}
                                    {ps.dataType === DataType.STRING &&
                                        ps.name !== 'CommandId' && (
                                            <TextField
                                                sx={{ minWidth: '60%' }}
                                                label={ps.displayName}
                                                variant="outlined"
                                                value={
                                                    currentUserCommand
                                                        .parameters[ps.name] ||
                                                    ps.default
                                                }
                                                required={ps.required}
                                                onChange={(event: any) =>
                                                    handleChange(
                                                        event,
                                                        ps.name,
                                                        true
                                                    )
                                                }
                                            />
                                        )}
                                    {(ps.dataType === DataType.DOUBLE ||
                                        ps.dataType === DataType.INTEGER) && (
                                        <TextField
                                            sx={{ minWidth: '60%' }}
                                            label={ps.displayName}
                                            variant="outlined"
                                            value={
                                                currentUserCommand.parameters[
                                                    ps.name
                                                ] || ps.default
                                            }
                                            required={ps.required}
                                            onChange={(event: any) =>
                                                handleChange(
                                                    event,
                                                    ps.name,
                                                    true
                                                )
                                            }
                                        />
                                    )}

                                    {ps.dataType === DataType.STRING &&
                                        ps.name === 'CommandId' && (
                                            <Tooltip
                                                title="Select the command to be executed."
                                                placement="right"
                                            >
                                                <FormControl
                                                    sx={{ minWidth: '60%' }}
                                                >
                                                    <InputLabel id="command-select-label">
                                                        Command
                                                    </InputLabel>
                                                    <Select
                                                        labelId="command-select-label"
                                                        value={
                                                            currentUserCommand
                                                                .parameters[
                                                                ps.name
                                                            ] || ps.default
                                                        }
                                                        label="Command"
                                                        required
                                                        variant="outlined"
                                                        onChange={(
                                                            event: any
                                                        ) =>
                                                            handleChange(
                                                                event,
                                                                ps.name,
                                                                true
                                                            )
                                                        }
                                                    >
                                                        {allUserCommands &&
                                                            allUserCommands.map(
                                                                (
                                                                    command: any,
                                                                    index: number
                                                                ) => (
                                                                    <MenuItem
                                                                        key={
                                                                            index
                                                                        }
                                                                        value={
                                                                            command
                                                                        }
                                                                    >
                                                                        <Typography>
                                                                            {
                                                                                command.name
                                                                            }
                                                                        </Typography>
                                                                    </MenuItem>
                                                                )
                                                            )}
                                                    </Select>
                                                </FormControl>
                                            </Tooltip>
                                        )}
                                </Box>
                            );
                        })}
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
