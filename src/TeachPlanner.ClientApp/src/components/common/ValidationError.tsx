type ValidationErrorProps = {
  errors: string[];
};

function ValidationError({ errors: validationErrors }: ValidationErrorProps) {
  return (
    <>
      {validationErrors.map((error) => (
        <p className="text-red-500">{error}</p>
      ))}
    </>
  );
}

export default ValidationError;
