# This file is responsible for configuring your application
# and its dependencies with the aid of the Mix.Config module.
#
# This configuration file is loaded before any dependency and
# is restricted to this project.

# General application configuration
use Mix.Config

# Configures the endpoint
config :smtp_server, SmtpServerWeb.Endpoint,
  url: [host: "localhost"],
  secret_key_base: "NDjguQTPdB8WL0kTQs2HsHGgpaA3OGjSZ0Xb5AqqpPOUFQ8n8JhBrU9yp2/OEXJF",
  render_errors: [view: SmtpServerWeb.ErrorView, accepts: ~w(json)],
  pubsub: [name: SmtpServer.PubSub, adapter: Phoenix.PubSub.PG2]

# Configures Elixir's Logger
config :logger, :console,
  format: "$time $metadata[$level] $message\n",
  metadata: [:request_id]

# Use Jason for JSON parsing in Phoenix
config :phoenix, :json_library, Jason

config :smtp_server, SmtpServerWeb.Mailer,
  adapter: Swoosh.Adapters.SMTP,
  relay: "smtp.gmail.com",
  username: "eota.error@gmail.com",
  #password: "EOTA08242019",
  password: "fgkbzypaaruggdds",
  ssl: false,
  tls: :if_available,
  auth: :always,
  port: 587,
  retries: 1

# Import environment specific config. This must remain at the bottom
# of this file so it overrides the configuration defined above.
import_config "#{Mix.env()}.exs"
