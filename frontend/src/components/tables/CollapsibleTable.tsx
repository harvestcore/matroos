import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import Box from '@mui/material/Box';
import Collapse from '@mui/material/Collapse';
import IconButton from '@mui/material/IconButton';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography, { TypographyProps } from '@mui/material/Typography';
import * as React from 'react';

import { Header } from './types';

function Row({
    row,
    rowHeaders,
    innerHeaders,
    innerRowsKey
}: {
    row: any;
    rowHeaders: any[];
    innerHeaders: any[];
    innerRowsKey: string;
}) {
    const [open, setOpen] = React.useState(false);

    const innerTableData = row[innerRowsKey];

    return (
        <React.Fragment>
            <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
                <TableCell>
                    <IconButton
                        aria-label="expand row"
                        size="small"
                        onClick={() => setOpen(!open)}
                    >
                        {open ? (
                            <KeyboardArrowUpIcon />
                        ) : (
                            <KeyboardArrowDownIcon />
                        )}
                    </IconButton>
                </TableCell>

                {rowHeaders.map((header: any, index: number) => {
                    let value = row[header.id];
                    return (
                        <TableCell key={index}>
                            <Typography
                                variant={'string' as TypographyProps['variant']}
                            >
                                {Array.isArray(value)
                                    ? value.join(', ')
                                    : value}
                            </Typography>
                        </TableCell>
                    );
                })}
            </TableRow>
            <TableRow>
                <TableCell
                    style={{ paddingBottom: 0, paddingTop: 0 }}
                    colSpan={6}
                >
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box sx={{ margin: 1 }}>
                            <Table size="small" aria-label="purchases">
                                <TableHead>
                                    <TableRow>
                                        {innerHeaders.map(
                                            (header: any, index: number) => (
                                                <TableCell
                                                    key={index}
                                                    component="th"
                                                    scope="row"
                                                    align={header.align}
                                                >
                                                    <Typography
                                                        variant={
                                                            'string' as TypographyProps['variant']
                                                        }
                                                    >
                                                        {header.displayName}
                                                    </Typography>
                                                </TableCell>
                                            )
                                        )}
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {innerTableData.map(
                                        (innerRow: any, index: number) => (
                                            <TableRow key={index}>
                                                {innerHeaders.map(
                                                    (
                                                        innerHeader: any,
                                                        innerIndex: number
                                                    ) => (
                                                        <TableCell
                                                            key={innerIndex}
                                                            scope="row"
                                                        >
                                                            <Typography
                                                                variant={
                                                                    'string' as TypographyProps['variant']
                                                                }
                                                            >
                                                                {
                                                                    innerRow[
                                                                        innerHeader
                                                                            .id
                                                                    ]
                                                                }
                                                            </Typography>
                                                        </TableCell>
                                                    )
                                                )}
                                            </TableRow>
                                        )
                                    )}
                                </TableBody>
                            </Table>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </React.Fragment>
    );
}

export default function CollapsibleTable({
    outerHeaders,
    outerRows,
    innerHeaders,
    innerRowsKey
}: {
    outerHeaders: Header[];
    outerRows: any[];
    innerHeaders: Header[];
    innerRowsKey: string;
}) {
    return (
        <TableContainer>
            <Table stickyHeader size="small">
                <TableHead>
                    <TableRow>
                        <TableCell key={0} />
                        {outerHeaders.map((header: any, index: number) => (
                            <TableCell key={index + 1} align={header.align}>
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
                    {outerRows.map((row: any, index: number) => (
                        <Row
                            key={index}
                            row={row}
                            rowHeaders={outerHeaders}
                            innerHeaders={innerHeaders}
                            innerRowsKey={innerRowsKey}
                        />
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}
