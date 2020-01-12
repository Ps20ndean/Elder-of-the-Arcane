# In this file, we load production configuration and secrets
# from environment variables. You can also hardcode secrets,
# although such is generally not recommended and you have to
# remember to add this file to your .gitignore.
use Mix.Config

config :smtp_server, SmtpServerWeb.Endpoint,
  http: [:inet6, port: String.to_integer("4096")],
  secret_key_base: "485kR8lJFurEZSqx7p1sx/rct84QEVnnTQzCOGN/+cLWZ2FsVboeLRGQlDN1zm3c"

# ## Using releases (Elixir v1.9+)
#
# If you are doing OTP releases, you need to instruct Phoenix
# to start each relevant endpoint:
#
#     config :smtp_server, SmtpServerWeb.Endpoint, server: true
#
# Then you can assemble a release by calling `mix release`.
# See `mix help release` for more information.
