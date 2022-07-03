import CircleIcon from '@mui/icons-material/Circle';
import Box from '@mui/material/Box';
import Icon from '@mui/material/Icon';
import IconButton from '@mui/material/IconButton';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell, { TableCellProps } from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';
import Tooltip, { TooltipProps } from '@mui/material/Tooltip';
import Typography, { TypographyProps } from '@mui/material/Typography';
import * as React from 'react';

import { Action, Header } from './types';

export default function SimpleTable({
    headers,
    rows,
    actions
}: {
    headers: Header[];
    rows: any[];
    actions?: Action[];
}) {
    const [page, setPage] = React.useState(0);
    const [rowsPerPage, setRowsPerPage] = React.useState(10);

    const handleChangePage = (_: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (
        event: React.ChangeEvent<HTMLInputElement>
    ) => {
        setRowsPerPage(+event.target.value);
        setPage(0);
    };

    if (
        actions &&
        actions.length > 0 &&
        !headers.some(item => item.id === 'actions')
    ) {
        headers.push({
            id: 'actions',
            displayName: 'Actions',
            align: 'center'
        });
    }

    return (
        <Box>
            <TableContainer>
                <Table stickyHeader size="small">
                    <TableHead>
                        <TableRow>
                            {headers.map((header: Header, index: number) => (
                                <TableCell
                                    key={index}
                                    align={
                                        (header.align as TableCellProps['align']) ||
                                        'left'
                                    }
                                >
                                    <Typography
                                        variant={
                                            'string' as TypographyProps['variant']
                                        }
                                    >
                                        {header.displayName}
                                    </Typography>
                                </TableCell>
                            ))}
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {rows
                            .slice(
                                page * rowsPerPage,
                                page * rowsPerPage + rowsPerPage
                            )
                            .map(row => {
                                return (
                                    <TableRow
                                        hover
                                        role="checkbox"
                                        tabIndex={-1}
                                        key={row.id}
                                    >
                                        {headers.map(
                                            (header: Header, index: number) => {
                                                const value = row[header.id];

                                                return (
                                                    <TableCell
                                                        key={index}
                                                        align={
                                                            (header.align as TableCellProps['align']) ||
                                                            'left'
                                                        }
                                                    >
                                                        {header.id ===
                                                            'actions' &&
                                                            actions &&
                                                            actions.map(
                                                                (
                                                                    action: Action,
                                                                    index: number
                                                                ) => (
                                                                    <Tooltip
                                                                        key={
                                                                            index
                                                                        }
                                                                        title={
                                                                            action.tooltip
                                                                        }
                                                                        placement={
                                                                            (action.tooltipDirection as TooltipProps['placement']) ||
                                                                            'right'
                                                                        }
                                                                    >
                                                                        <IconButton
                                                                            onClick={() =>
                                                                                action.handleClick(
                                                                                    row.id
                                                                                )
                                                                            }
                                                                        >
                                                                            <Icon
                                                                                sx={{
                                                                                    gap: 0,
                                                                                    color:
                                                                                        action.iconColor ||
                                                                                        '#3a44b5'
                                                                                }}
                                                                            >
                                                                                {
                                                                                    action.icon
                                                                                }
                                                                            </Icon>
                                                                        </IconButton>
                                                                    </Tooltip>
                                                                )
                                                            )}

                                                        {Array.isArray(value) &&
                                                            header.id !==
                                                                'actions' && (
                                                                <Typography
                                                                    variant={
                                                                        'string' as TypographyProps['variant']
                                                                    }
                                                                >
                                                                    {
                                                                        value.length
                                                                    }
                                                                </Typography>
                                                            )}
                                                        {!Array.isArray(
                                                            value
                                                        ) &&
                                                            header.id !==
                                                                'actions' && (
                                                                <Typography
                                                                    variant={
                                                                        'string' as TypographyProps['variant']
                                                                    }
                                                                >
                                                                    {value}
                                                                </Typography>
                                                            )}
                                                        {typeof value ===
                                                            'boolean' && (
                                                            <Tooltip
                                                                title={
                                                                    value
                                                                        ? 'Alive.'
                                                                        : 'Dead.'
                                                                }
                                                                placement="top"
                                                            >
                                                                <CircleIcon
                                                                    sx={{
                                                                        color: value
                                                                            ? '#44b033'
                                                                            : '#b02537'
                                                                    }}
                                                                />
                                                            </Tooltip>
                                                        )}
                                                    </TableCell>
                                                );
                                            }
                                        )}
                                    </TableRow>
                                );
                            })}
                    </TableBody>
                </Table>
            </TableContainer>

            <TablePagination
                rowsPerPageOptions={[10, 25, 100]}
                component="div"
                count={(rows && rows.length) || 0}
                rowsPerPage={rowsPerPage}
                page={page}
                onPageChange={handleChangePage}
                onRowsPerPageChange={handleChangeRowsPerPage}
            />
        </Box>
    );
}
