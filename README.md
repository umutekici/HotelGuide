Hotel Guide Tutorial

This project includes two microservices named hotel and report.

Rabbit mq was added to the project to provide communication between the two microservices.

The project is running on docker.

docker run -d -p 1453:15672 -p 5672:5672 --name rabbitmqcontainer rabbitmq:3-management

A sample screenshot to explain the project:

![Docker](https://github.com/user-attachments/assets/a5c67334-2d4b-4551-a7d8-9a5f58fee229)

Let's look at a sample report request that returns hotels in Istanbul:

![reportRequest](https://github.com/user-attachments/assets/961be1d9-9f28-45ae-b4bb-c8923c2e9b61)

When the request reaches the report controller:

![reportController](https://github.com/user-attachments/assets/51edc569-5fde-4d29-a8d3-e87e2f7d92d9)

After that The request is queued via rabbitmq.

When a report request is generated, this request is sent to rabbitmq.

Sample image of the request arriving at the queue:

![rabbitmq](https://github.com/user-attachments/assets/564b6976-3aab-4682-968d-960106a0c28e)

Thank you for reading.


