
# Pentagonal

Pentagonal for [NFx](https://github.com/NancyFx/Nancy)

## About

Pentagonal is a collection of libraries and services
that take care of a lot of core components of a web application,
so you can focus on *your* app instead of worrying about
details. See the features list.

## Setup

You can choose whatever services you want to install
into your NFx app and install them in the Bootstrapper.

See the [documentation](https://github.com/0xFireball/Pentagonal/wiki/Getting-Started) for a Quick Start!

## Features

Pentagonal does a lot of stuff for you.

- Authentication (`Pentagonal.Auth`)
  - Standard authentication (username, password)
    - Installs API to `/u/`
      - See documentation for more information
    - Uses secure, modern password hashing
  - Stateless authentication
    - Authentication provided by API key
    - Each user gets an API key
      - API key can be regenerated
  - Includes user manager to query and access user data from your app
- Utilities/Core (`Pentagonal`)
  - Concurrency tools and user locks
    - Prevent race conditions accessing user data
    - Throttle resource usage
      - Add your own custom throttles
  - JSON.NET Serializer
  - Wildcard matcher


## License

Copyright 2017 &copy; 0xFireball (Nihal Talur). All Rights Reserved.

Licensed under the Apache License 2.0.
