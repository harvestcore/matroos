import Box from '@mui/material/Box';
import Chip from '@mui/material/Chip';
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import OutlinedInput from '@mui/material/OutlinedInput';
import Select from '@mui/material/Select';

import { ChipSelectorProps } from './types';

const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
    PaperProps: {
        style: {
            maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
            width: 250
        }
    }
};

export default function ChipSelector({
    items,
    selectedItems,
    idItemProperty,
    displayNameItemProperty,
    fieldInputId,
    fieldInputDisplayName,
    handleChange = () => {}
}: ChipSelectorProps) {
    return (
        <div>
            <FormControl sx={{ width: '100%' }}>
                <InputLabel>{fieldInputDisplayName}</InputLabel>
                <Select
                    multiple
                    value={selectedItems}
                    onChange={handleChange}
                    input={
                        <OutlinedInput
                            id={fieldInputId}
                            label={fieldInputDisplayName}
                        />
                    }
                    renderValue={selected => (
                        <Box
                            sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}
                        >
                            {selected.map((value: any) => (
                                <Chip
                                    key={value[idItemProperty]}
                                    label={value[displayNameItemProperty]}
                                />
                            ))}
                        </Box>
                    )}
                    MenuProps={MenuProps}
                >
                    {items.map((item: any, index: number) => (
                        <MenuItem key={index} value={item}>
                            {item[displayNameItemProperty]}
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>
        </div>
    );
}
