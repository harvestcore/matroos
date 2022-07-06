import Box from '@mui/material/Box';
import * as React from 'react';

import { CommandModeToString } from '../../utils/commandMode';
import { CommandTypeToString } from '../../utils/commandType';
import { CommandSignature, Guid, UserCommand } from '../../utils/types';
import SimpleTable from '../tables/SimpleTable';
import EditCommandModal from './EditCommandModal';
import { TabPanelProps } from './types';

export default function UserCommandsTabPanel({
    value,
    index,
    data,
    commandSignatures
}: TabPanelProps) {
    const [modalIsOpen, setModalIsOpen] = React.useState<boolean>(false);
    const [selectedUserCommand, setSelectedUserCommand] =
        React.useState<UserCommand>();

    const handleClick = (id: Guid) => {
        setSelectedUserCommand(data.find(item => item.id === id));
        setModalIsOpen(true);
    };

    const handleModalClose = () => {
        setSelectedUserCommand(undefined);
        setModalIsOpen(false);
    };

    const newItems: any = [];

    data.forEach(item => {
        newItems.push({
            ...item,
            type: CommandTypeToString(item.type),
            mode: CommandModeToString(item.mode)
        });
    });

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`simple-tabpanel-${index}`}
        >
            {value === index && (
                <Box sx={{ p: 3 }}>
                    <SimpleTable
                        headers={[
                            { id: 'name', displayName: 'Name' },
                            { id: 'description', displayName: 'Description' },
                            {
                                id: 'trigger',
                                displayName: 'Trigger',
                                align: 'center'
                            },
                            {
                                id: 'type',
                                displayName: 'Type',
                                align: 'center'
                            },
                            {
                                id: 'mode',
                                displayName: 'Mode',
                                align: 'center'
                            },
                            { id: 'updatedAt', displayName: 'Last update' }
                        ]}
                        rows={newItems}
                        actions={[
                            {
                                id: 'edit',
                                icon: 'info',
                                handleClick: handleClick,
                                tooltip: 'See details'
                            }
                        ]}
                    />

                    {selectedUserCommand && (
                        <EditCommandModal
                            open={modalIsOpen}
                            onClose={handleModalClose}
                            userCommand={selectedUserCommand}
                            commandSignatures={
                                commandSignatures as CommandSignature[]
                            }
                            allUserCommands={data}
                        />
                    )}
                </Box>
            )}
        </div>
    );
}
