defmodule SmtpServerWeb.Router do
  use SmtpServerWeb, :router

  pipeline :api do
    plug :accepts, ["json"]
  end

  scope "/smtp", SmtpServerWeb do
    pipe_through :api

    post "/send", MailerController, :send
  end
end
