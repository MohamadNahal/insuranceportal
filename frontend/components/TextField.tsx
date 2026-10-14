
type TextFieldProps = {
  id: string;
  label: string;
  value: string;
  onChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
  error?: string;
  type?: string;
  maxLength?: number;
};

export default function TextField({
  id,
  label,
  value,
  onChange,
  error = "",
  type = "text",
  maxLength,
}: TextFieldProps) {
  return (
    <div>
      <label
        htmlFor={id}
        className="mb-2 block text-sm font-semibold text-gray-800"
      >
        {label}
      </label>

      <input
        id={id}
        name={id}
        type={type}
        value={value}
        onChange={onChange}
        maxLength={maxLength}
        aria-invalid={Boolean(error)}
        aria-describedby={error ? `${id}-error` : undefined}
        className={`w-full rounded-md border bg-white px-3 py-2 text-gray-800 outline-none focus:ring-2 ${
          error
            ? "border-red-500 focus:border-red-500 focus:ring-red-200"
            : "border-gray-300 focus:border-blue-500 focus:ring-blue-200"
        }`}
      />

      {error && (
        <p
          id={`${id}-error`}
          role="alert"
          className="mt-1 text-sm font-medium text-red-500"
        >
          {error}
        </p>
      )}
    </div>
  );
}
