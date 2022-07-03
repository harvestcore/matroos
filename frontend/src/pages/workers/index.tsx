import CachedIcon from '@mui/icons-material/Cached';
import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';
import { useCallback, useEffect, useState } from 'react';

import styles from '../../Home.module.css';
import { getAllBots } from '../../api/bots';
import { getAllWorkers } from '../../api/workers';
import SimpleTable from '../../components/tables/SimpleTable';
import EditWorkerModal from '../../components/workers/EditWorkerModal';
import { Bot, ItemsResponse, Worker } from '../../utils/types';

export default function WorkersHome() {
    const [workers, setWorkers] = useState<ItemsResponse<Worker>>({
        count: 0,
        items: []
    });
    const [bots, setBots] = useState<ItemsResponse<Bot>>({
        count: 0,
        items: []
    });
    const [editModalIsOpen, setEditModalIsOpen] = useState<boolean>(false);
    const [selectedWorker, setSelectedWorker] = useState<Worker>();

    const handleEditClick = (id: string) => {
        setSelectedWorker(
            workers.items.find((worker: Worker) => worker.id === id)
        );
        setEditModalIsOpen(true);
    };

    const handleModalClose = () => {
        setSelectedWorker(undefined);
        setEditModalIsOpen(false);
        fetchData();
    };

    const fetchData = useCallback(async () => {
        const workers: ItemsResponse<Worker> = await getAllWorkers();
        const bots: ItemsResponse<Bot> = await getAllBots();

        setWorkers(workers);
        setBots(bots);
    }, [selectedWorker]);

    useEffect(() => {
        fetchData();
    }, [fetchData]);

    return (
        <div className={styles.container}>
            <main className={styles.main}>
                <h1 className={styles.title}>Workers</h1>

                <Box sx={{ width: '100%' }}>
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
                    </Box>

                    <SimpleTable
                        headers={[
                            {
                                id: 'isUp',
                                displayName: 'Status',
                                align: 'center'
                            },
                            { id: 'remoteUrl', displayName: 'Remote URL' },
                            { id: 'lastUpdate', displayName: 'Last update' },
                            { id: 'bots', displayName: 'Bots', align: 'center' }
                        ]}
                        rows={workers.items}
                        actions={[
                            {
                                id: 'edit',
                                icon: 'info',
                                handleClick: handleEditClick,
                                tooltip: 'See details'
                            }
                        ]}
                    />

                    {selectedWorker && (
                        <EditWorkerModal
                            open={editModalIsOpen}
                            onClose={handleModalClose}
                            worker={selectedWorker}
                            bots={bots.items}
                        />
                    )}
                </Box>
            </main>
        </div>
    );
}
