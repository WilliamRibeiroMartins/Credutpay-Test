# Credutpay-Test
Teste tecnico para a Credutpay:
	API REST de carteira digital com funcionalidades de autenticação, transferência de saldo entre usuários, e histórico de transações.

---

## ✅ Requisitos

- [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- (Opcional) [pgAdmin 4](https://www.pgadmin.org/download/) para gerenciar o banco graficamente
- (Opcional) [Insomnia](https://insomnia.rest/download) ou [Postman](https://www.postman.com/downloads/) para testar os endpoints

---

## 🚀 Como executar o projeto

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/Credutpay-Test.git
cd Credutpay-Test
```

---

### 2. Configure o banco de dados

1. Crie um banco chamado `Credutpay` no PostgreSQL (pelo pgAdmin ou via terminal).

2. No arquivo `appsettings.json`, configure a `ConnectionStrings` com o usuário e porta corretos:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=Credutpay;Username=postgres;Password=sua_senha"
}
```

> ⚠️ Se você não usa senha no PostgreSQL, configure o `pg_hba.conf` com método `trust` (não recomendado em produção).

---

### 3. Restore e compile o projeto

```bash
dotnet restore
dotnet build
```

---

### 4. Execute as migrations e rode a aplicação

```bash
dotnet ef database update
dotnet run
```

Ao iniciar, o projeto:
- Cria o banco (se não existir)
- Aplica as migrations
- Popula com 2 usuários: `alice` e `bob`, senha `123` (criptografada)

---

## 🔑 Autenticação

Use o endpoint de login para obter um token JWT:

```
POST /auth/login
{
  "username": "alice",
  "password": "123"
}
```

O token JWT retornado deve ser usado no cabeçalho das requisições protegidas:

```
Authorization: Bearer <seu_token_aqui>
```

---

## 📮 Endpoints e Requisições

- `POST /auth/register` — cria um novo usuário
```cURL
curl --request POST \
  --url https://localhost:7001/api/user/register \
  --header 'Content-Type: application/json' \
  --data '{
  "username": "william",
  "password": "123456"
}'
```

- `POST /auth/login` — autentica e retorna token JWT
```cURL
curl --request POST \
  --url https://localhost:7001/api/auth/login \
  --header 'Content-Type: application/json' \
  --data '{
  "username": "alice",
  "password": "123"
}'
```

- `POST /transfer` — transfere saldo de um usuário para outro
```cURL
curl --request POST \
  --url https://localhost:7001/api/transfer \
  --header 'Authorization: Bearer seu-token-jwt' \
  --header 'Content-Type: application/json' \
  --data '{
  "toUserId": "1952852d-24cd-485b-8f67-da0e89d1eb4f",
  "amount": 50.0
}'
```

- `GET /transactions` — lista histórico de transações do usuário autenticado
```cURL
curl --request GET \
  --url https://localhost:7001/api/transfer \
  --header 'Authorization: Bearer seu-token-jwt' \
  --header 'Content-Type: application/json' \
  --data '{
  "startDate": "2025-01-01T00:00:00",
  "endDate": "2025-12-31T23:59:59"
}'
```

- `GET /balance` — consulta saldo
```cURL
curl --request GET \
  --url https://localhost:7001/api/user/balance \
  --header 'Authorization: Bearer seu-token-jwt' \
  --header 'Content-Type: application/json'
```

---

## 🛠️ Scripts úteis
 
- Atualizar o banco com as migrations:

```bash
dotnet ef database update
```

- Rodar a aplicação:
```bash
dotnet run
```

