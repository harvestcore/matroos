import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import CachedIcon from '@mui/icons-material/Cached';
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';
import { useCallback, useEffect, useState } from 'react';

import styles from '../../Home.module.css';
import { getAllBots } from '../../api/bots';
import { getAllUserCommands } from '../../api/commands';
import CreateBotModal from '../../components/bots/CreateBotModal';
import EditBotModal from '../../components/bots/EditBotModal';
import SimpleTable from '../../components/tables/SimpleTable';
import { Bot, ItemsResponse, UserCommand } from '../../utils/types';

export default function BotsHome() {
    const [bots, setBots] = useState<ItemsResponse<Bot>>({
        count: 0,
        items: []
    });
    const [commands, setCommands] = useState<ItemsResponse<UserCommand>>({
        count: 0,
        items: []
    });
    const [editModalIsOpen, setEditModalIsOpen] = useState(false);
    const [createModalIsOpen, setCreateModalIsOpen] = useState(false);
    const [selectedBot, setSelectedBot] = useState<Bot>();

    const handleEditClick = (id: string) => {
        setSelectedBot(bots.items.find(item => item.id === id));
        setEditModalIsOpen(true);
    };

    const handleModalClose = () => {
        setSelectedBot(undefined);
        setEditModalIsOpen(false);
        setCreateModalIsOpen(false);
        fetchData();
    };

    const handleAddClick = () => {
        setCreateModalIsOpen(true);
    };

    const handleReload = () => {
        fetchData();
    };

    const fetchData = useCallback(async () => {
        const data: ItemsResponse<Bot> = await getAllBots();
        const allCommands: ItemsResponse<UserCommand> =
            await getAllUserCommands();

        setBots(data);
        setCommands(allCommands);
    }, [selectedBot]);

    useEffect(() => {
        fetchData();
    }, [fetchData]);

    return (
        <div className={styles.container}>
            <main className={styles.main}>
                <h1 className={styles.title}>Bots</h1>

                <Box sx={{ width: '100%' }}>
                    <Box>
                        <Tooltip title="Sync" placement="top">
                            <IconButton
                                onClick={() => handleReload()}
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

                    <SimpleTable
                        headers={[
                            { id: 'name', displayName: 'Name' },
                            { id: 'description', displayName: 'Description' },
                            { id: 'key', displayName: 'Discord Key' },
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
                        rows={bots.items}
                        actions={[
                            {
                                id: 'edit',
                                icon: 'info',
                                handleClick: handleEditClick,
                                tooltip: 'See details'
                            }
                        ]}
                    />

                    {selectedBot && (
                        <EditBotModal
                            open={editModalIsOpen}
                            onClose={handleModalClose}
                            bot={selectedBot}
                            commands={commands.items}
                        />
                    )}

                    <CreateBotModal
                        open={createModalIsOpen}
                        onClose={handleModalClose}
                        commands={commands.items}
                    />
                </Box>
            </main>
        </div>
    );
}
