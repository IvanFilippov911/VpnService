# DrakarVPN 

Проект представляет собой индивидуальную разработку полнофункционального VPN-сервиса с панелью управления, подписками, логированием и интеграцией с WireGuard. Выполнен в рамках учебной практики КФУ ИТИС.

---

## Стек технологий

- ASP.NET Core 8 (Backend)
- PostgreSQL (основная БД)
- Redis (Rate Limiting, кеш)
- MongoDB (логирование)
- SignalR (уведомления)
- Identity + JWT
- WireGuard (VPN уровень)
- Docker + Docker Compose

---

## Быстрый старт

### 1. Клонирование репозитория

```bash
git clone https://github.com/your-username/drakarvpn.git
cd drakarvpn

2. appsettings.Development.json (вставьте это)
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },

  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5435;Database=drakarvpn_dev;Username=postgres;Password=postgres"
  },

  "AuthSettings": {
    "SecretKey": "4f7690e4a766613ab1f314b7bfd609fe1972dd0ee88c4e69240447621fcb5d137045e846f47d2ff7f10cb0fd10ef623ecbba21e63fad3a81be8681c8ee10501f"
  },

  "WireGuardConfig": {
    "ConfigFilePath": "/etc/wireguard/wg0.conf",
    "ServerPublicKey": "rxOUEAQaxRNHd+c+zmB8hdLorda1UPwsy4pnA67GmCo=",
    "Endpoint": "77.221.153.119:51820"
  },

  "Mongo": {
    "ConnectionString": "mongodb://localhost:27017",
    "Database": "DrakarVpnDb",
    "Collections": {
      "UserLogs": "vpn_user_logs",
      "SystemLogs": "vpn_system_logs"
    }
  }
}


3. Запуск инфраструктуры
docker-compose up -d


4. Применения миграций

5. После запуска основное API доступно на https://localhost:7218/swagger


Настройка VPN-сервера (WireGuard)

1. Установка на Ubuntu
sudo apt update
sudo apt install -y wireguard

2. Генерация ключей
umask 077
wg genkey | tee /etc/wireguard/privatekey | wg pubkey > /etc/wireguard/publickey

3. Создание конфигурации /etc/wireguard/wg0.conf
[Interface]
PrivateKey = <PRIVATE_KEY>
Address = 10.8.0.1/24
ListenPort = 51820
PostUp = iptables -A FORWARD -i wg0 -j ACCEPT; iptables -t nat -A POSTROUTING -o eth0 -j MASQUERADE
PostDown = iptables -D FORWARD -i wg0 -j ACCEPT; iptables -t nat -D POSTROUTING -o eth0 -j MASQUERADE

Замените <PRIVATE_KEY> на содержимое /etc/wireguard/privatekey

4. Включение IP forwarding
sudo sysctl -w net.ipv4.ip_forward=1
echo "net.ipv4.ip_forward=1" | sudo tee -a /etc/sysctl.conf
sudo sysctl -p

5. Запуск сервиса
sudo systemctl start wg-quick@wg0
sudo systemctl enable wg-quick@wg0

Проверка:
sudo wg


Создание VPN-устройств
Пользователь отправляет publicKey(с клиента wireguard) → сервер выдаёт IP-адрес и WireGuard-конфигурацию с данными сервера.


