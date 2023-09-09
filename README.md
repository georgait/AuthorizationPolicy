# AuthorizationPolicy

This project is a proof of concept that demonstrates the integration of a policy (library) server, a payment gateway, and a secured API using RabbitMQ for asynchronous communication.

The project was conceived to address a common challenge in applications where dynamic authorization permissions need to be applied without requiring the user to log out. For instance, consider a scenario where certain conditions need to be met before a user can access specific resources. In such cases, it's crucial to restrict access until the conditions are met, all while ensuring a seamless user experience. This project serves as an example of how such scenarios can be handled effectively.

## Components

### Policy (Library) Server

At the core of our project is the policy server, based on the [PolicyServer.Local](https://github.com/PolicyServer/PolicyServer.Local) project. This server manages and enforces access control policies, ensuring that only authorized users can access certain resources.

### Payment Gateway

The PaymentGateway API simulates a paid service. It features two main endpoints:

1. **Virtual Payment Endpoint**: This endpoint simulates the process of making a payment.
2. **Payment Information Endpoint**: This endpoint retrieves information about a specific payment by its ID.

### Web API

The WebApi evaluates the claims of the user and determines whether or not they have access to a secured API. The decision is based on whether or not certain conditions are met.

## Communication

The PaymentGateway and WebApi communicate asynchronously via RabbitMQ, a robust messaging system for applications. This ensures smooth and efficient communication between the two APIs, even under heavy load.

## Integration with RabbitMQ using Docker

Here are the instructions to integrate RabbitMQ using Docker:

1. **Install Docker**: If you haven't already, you'll need to [install Docker](https://docs.docker.com/get-docker/) on your machine.

2. **Pull the RabbitMQ image**: Open a terminal and run the following command to pull the latest RabbitMQ image from Docker Hub:
   ```
   docker pull rabbitmq:3-management
   ```
   The `3-management` tag ensures that you're pulling the RabbitMQ image with management plugin enabled.

3. **Run the RabbitMQ container**: Run the following command to start a new RabbitMQ container:
   ```
   docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management
   ```
   This command does the following:
   - `-d` runs the container in detached mode (in the background).
   - `--hostname my-rabbit` and `--name some-rabbit` set the hostname and name of the container.
   - `-p 5672:5672` and `-p 15672:15672` publish the container's ports to the host. Port 5672 is used by RabbitMQ for messaging, and port 15672 is used by the management plugin for its web interface.

4. **Verify the RabbitMQ container is running**: You can verify that your RabbitMQ container is running by visiting `http://localhost:15672` in your web browser. You should see the RabbitMQ management interface. The default username and password are both `guest`.

5. **Configure your application**: In your application's configuration file, you should have something like this (it's already there):
   ```
   "RabbitMQ": {
     "Host": "localhost",
     "Port": "5672"
   },
   ```
   This tells your application to connect to RabbitMQ at `localhost` on port `5672`.

And that's it! Your application should now be able to connect to RabbitMQ running in a Docker container on your machine.

## Disclaimer

Please note that this project is not meant for production use. Use it to experiment and have fun, but use it at your own risk when it comes to production environments.
