# DC
 Distributed computing

## Lab 1
### Requirements:
#### Data Model
##### Entities
 * Author
 * Tweet
 * Marker
 * Post
#### DTOs
##### Request DTOs
 * AuthorRequestTo
 * TweetRequestTo
 * MarkerRequestTo
 * PostRequestTo
##### Response DTOs
 * AuthorResponseTo
 * TweetResponseTo
 * MarkerResponseTo
 * PostResponseTo
##### Storage Layer
 * Interface
 * Create a generalized interface for storing and retrieving entity data for CRUD operations.

#### InMemory Implementation
 * Implement an in-memory storage solution using a Map collection for the Author, Tweet, Marker, and Post entities.

#### Services Layer
 * Create services for the Author, Tweet, Marker, and Post entities. Implement business logic (CRUD operations) in the services to process the data of the objects.
 * The services should also handle the conversion of DTO objects into entities (for requests) and vice versa (for responses). You can consider using an off-the-shelf solution like MapStruct for this purpose.

#### Controller Layer
 * Create REST controllers for the Author, Tweet, Marker, and Post objects. The controllers should adhere to RESTful principles and generate appropriate error responses if constraints are violated.
 * Consider implementing an exception handler for the REST controllers using the @ControllerAdvice annotation.
 * The REST controllers should support the following CRUD operations:
    * Searching 
        * Author
        * Tweet
        * Marker
    * Post by the id field
    * Creating 
        * Author
        * Tweet 
        * Marker 
        * Post entities
    * Modifying 
        * Author
        * Tweet
        * Marker
        * Post entities
    * Deleting 
        * Author
        * Tweet
        * Marker
        * Post entities by the id field


