defmodule SmtpServerWeb.MailerController do
  use SmtpServerWeb, :controller

  def send(conn, params) do
    IO.inspect params

    sent_email =
    SmtpServerWeb.Email.make_crash_email("Test Name", "jaytechmedia@googlemail.com", "Crash Report", "The body of the email!")
    |> SmtpServerWeb.Mailer.deliver()

    case sent_email do
      {:ok, _email} ->  send_resp(conn, 200, "")
      {:error, error} ->  send_resp(conn, 400, "Some Error : #{inspect(error)}")
    end
  end
end

defmodule SmtpServerWeb.Mailer do
  use Swoosh.Mailer, otp_app: :smtp_server
end

defmodule SmtpServerWeb.Email do
  import Swoosh.Email

  def make_crash_email(to_name, to_email, subject, body) do
    new()
    |> to({to_name, to_email})
    |> from({"Elder of The Arcane", "eota.error@gmail.com"})
    |> subject(subject)
    |> text_body(body)
  end
end
