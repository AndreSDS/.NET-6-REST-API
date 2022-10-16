# Buber Dinner API

- [Buber Dinner API](#buber-dinner-api)
- [Auth](#auth)
  - [Register](#register)
  - [Register Request](#register-request)
  - [Register Response](#register-response)
  - [Login](#login)
  - [Login Request](#login-request)
  - [Login Response](#login-response)

## Auth

### Register

```js
POST {{host}}/auth/register
```

#### Register Request

```json
{
  "firstName": "André",
  "lastName": "Gomes",
  "email": "andre@email.com",
  "password": "123456"
}
```

#### Register Response

```js
200 OK
```

```json
{
  "id": "5f9f9f9f9f9f9f9f9f9f9f9f",
  "firstName": "André",
  "lastName": "Gomes",
  "email": "andre@email.com",
  "token": "eyJhb..hbbQ"
}
```

### Login

```js
POST {{host}}/auth/login
```

#### Login Request

```json
{
  "email": "andre@email.com",
  "password": "123456"
}
```

#### Login Response

```json
{
  "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
  "firstName": "John",
  "lastName": "Doe",
  "email": "johndoe@email.com",
  "token": "eyJhb..hbbQ"
}
```
