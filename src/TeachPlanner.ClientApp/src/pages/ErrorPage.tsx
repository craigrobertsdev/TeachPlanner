import { useRouteError } from "react-router-dom";

const ErrorPage = () => {
  return (
    <div className="flex flex-col flex-auto items-center justify-center text-darkGreen">
      <h1>Oops!</h1>
      <p>Sorry, an unexpected error has occurred.</p>
    </div>
  );
};

export default ErrorPage;
