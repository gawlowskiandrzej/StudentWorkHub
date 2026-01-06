import { ChangeEventHandler } from "react";


type FloatingLabelInputProps = {
  type?: string;
  value?: string | number | readonly string[] | undefined;
  onChange?: ChangeEventHandler<HTMLInputElement> | undefined;
  label: string;
};

export function FloatingLabelInput({ type = "text", label, value, onChange }: FloatingLabelInputProps) {

  return (
    <div className="relative w-full">
    <input value={value} type={type} onChange={onChange} id={`floating_outlined${label}`} className="block px-2.5 pb-2.5 pt-4 w-full text-sm text-heading bg-background rounded-base border-1 border-default-medium appearance-none focus:outline-none focus:ring-0 focus:border-brand peer" placeholder=" " />
    <label htmlFor={`floating_outlined${label}`} className="absolute select-none text-sm text-body duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-background px-2 peer-focus:px-2 peer-focus:text-fg-brand peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto start-1 text-secondary">{label}</label>
</div>
  );
}