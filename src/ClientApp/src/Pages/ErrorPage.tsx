import { useRouteError } from "react-router-dom";

const ErrorPage = () => {
  const error = useRouteError() as { statusText?: string; message?: string };
  console.log(error);

  return (
    <div className="flex flex-col flex-auto items-center justify-center text-darkGreen">
      <h1>Oops!</h1>
      <p>Sorry, an unexpected error has occurred.</p>
      <p>
        <i>{error?.statusText || error?.message}</i>
      </p>
    </div>
  );
};

export default ErrorPage;
