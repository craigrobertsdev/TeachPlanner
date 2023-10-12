type DeleteButtonProps = {
  onClick: React.MouseEventHandler<HTMLButtonElement>;
};

function CancelButton({ onClick }: DeleteButtonProps) {
  return (
    <div>
      <button className="float-right text-2xl text-ceramic" onClick={onClick}>
        &#10006;
      </button>
    </div>
  );
}

export default CancelButton;
