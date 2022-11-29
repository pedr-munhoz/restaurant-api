# restaurant-api
An demo api used to experiment with messaging as a main way for communication between different parts of the system to deal with long running operations.

No method calls between classes - only messages and queues.

The user starts a chain of processes with a http request and in the end recieves a webhook with the full result.

