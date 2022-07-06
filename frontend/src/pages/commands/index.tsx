import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import CachedIcon from '@mui/icons-material/Cached';
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import Tab from '@mui/material/Tab';
import Tabs from '@mui/material/Tabs';
import Tooltip from '@mui/material/Tooltip';
import { useCallback, useEffect, useState } from 'react';

import styles from '../../Home.module.css';
import { getAllUserCommands, getCommandsSignature } from '../../api/commands';
import CreateCommandModal from '../../components/commands/CreateCommandModal';
import TemplateCommandsTabPanel from '../../components/commands/TemplateCommandsTabPanel';
import UserCommandsTabPanel from '../../components/commands/UserCommandsTabPanel';
import {
    CommandSignature,
    ItemsResponse,
    UserCommand
} from '../../utils/types';

export default function CommandsHome() {
    const [commandSignatures, setCommandSignatures] = useState<
        ItemsResponse<CommandSignature>
    >({
        count: 0,
        items: []
    });
    const [userCommands, setUserCommands] = useState<
        ItemsResponse<UserCommand>
    >({
        count: 0,
        items: []
    });
    const [value, setValue] = useState(0);
    const [createModalIsOpen, setCreateModalIsOpen] = useState<boolean>(false);

    const handleChange = (_: any, newValue: number) => {
        setValue(newValue);
    };

    const handleAddClick = () => {
        setCreateModalIsOpen(true);
    };

    const handleModalClose = () => {
        setCreateModalIsOpen(false);
        fetchData();
    };

    const fetchData = useCallback(async () => {
        const commandSignatures: ItemsResponse<CommandSignature> =
            await getCommandsSignature();
        const userCommands: ItemsResponse<UserCommand> =
            await getAllUserCommands();

        setCommandSignatures(commandSignatures);
        setUserCommands(userCommands);
    }, [createModalIsOpen]);

    useEffect(() => {
        fetchData();
    }, [fetchData]);

    return (
        <div className={styles.container}>
            <main className={styles.main}>
                <h1 className={styles.title}>Commands</h1>

                <Box
                    sx={{
                        width: '100%',
                        gap: 2,
                        display: 'flex',
                        flexDirection: 'column'
                    }}
                >
                    <Box>
                        <Tooltip title="Sync" placement="top">
                            <IconButton
                                onClick={
                                    () => {} /*Router.replace(Router.asPath)*/
                                }
                                size="large"
                            >
                                <CachedIcon />
                            </IconButton>
                        </Tooltip>
                        <Tooltip title="Create command" placement="top">
                            <IconButton onClick={handleAddClick} size="large">
                                <AddCircleOutlineIcon />
                            </IconButton>
                        </Tooltip>
                    </Box>

                    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                        <Tabs value={value} onChange={handleChange}>
                            <Tab label="User commands" />
                            <Tab label="Templates" />
                        </Tabs>
                    </Box>

                    <UserCommandsTabPanel
                        value={value}
                        index={0}
                        data={userCommands.items}
                        commandSignatures={commandSignatures.items}
                    />

                    <TemplateCommandsTabPanel
                        value={value}
                        index={1}
                        data={commandSignatures.items}
                    />

                    <CreateCommandModal
                        open={createModalIsOpen}
                        onClose={handleModalClose}
                        commandSignatures={commandSignatures.items}
                        allUserCommands={userCommands.items}
                    />
                </Box>
            </main>
        </div>
    );
}
