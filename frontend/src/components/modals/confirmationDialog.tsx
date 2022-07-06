import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogTitle from '@mui/material/DialogTitle';

export type ConfirmationDialogProps = {
    open: boolean;
    handleClose: (decision: boolean) => void;
    title?: string;
    confirmLabel?: string;
    cancelLabel?: string;
};

export default function ConfirmationDialog({
    handleClose,
    open,
    title,
    confirmLabel,
    cancelLabel
}: ConfirmationDialogProps) {
    return (
        <Box sx={{ width: '100%', maxWidth: 360 }}>
            <Dialog
                sx={{ '& .MuiDialog-paper': { width: '80%', maxHeight: 435 } }}
                maxWidth="xs"
                open={open}
            >
                <DialogTitle>{title || 'Are you sure?'}</DialogTitle>
                <DialogActions>
                    <Box
                        sx={{
                            marginX: '1em',
                            marginBottom: '1em',
                            width: '100%',
                            display: 'flex',
                            flexDirection: 'row',
                            justifyContent: 'space-between'
                        }}
                    >
                        <Button
                            variant="outlined"
                            color="error"
                            onClick={() => handleClose(true)}
                        >
                            {confirmLabel || 'Yes'}
                        </Button>
                        <Button
                            variant="outlined"
                            onClick={() => handleClose(false)}
                        >
                            {cancelLabel || 'Cancel'}
                        </Button>
                    </Box>
                </DialogActions>
            </Dialog>
        </Box>
    );
}
