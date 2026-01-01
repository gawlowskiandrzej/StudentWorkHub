export function toLabelValueFormat(array : string[]) {
  return array.map(item => ({
    label: item,
    value: item
  }));
}