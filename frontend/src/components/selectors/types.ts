export type ChipSelectorProps = {
    items: any[];
    selectedItems: any[];
    idItemProperty: string;
    displayNameItemProperty: string;
    fieldInputId: string;
    fieldInputDisplayName: string;
    handleChange?: (event: any) => void;
};
