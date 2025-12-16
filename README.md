# Настройка Redis

1. Для настройки Redis вводим команду 
```
nano /etc/redis/redis.conf
```

2. Изменяем строки подобным образом:
<img width="306" height="96" alt="image" src="https://github.com/user-attachments/assets/677276f6-b788-4b14-9ea0-d61fbf19a14a" />

Здесь задан порт, на котором будет расположена БД и пароль "1"

3. ⛔ Обязательно перезапускаем БД, потому что может не заработать
```
systemctl restart redis
```

4. Теперь можно подключаться к нашей БД в коде

# Подключение Redis в C#
```
builder.Services.AddSingleton<IConnectionMultiplexer>(
    //ConnectionMultiplexer.Connect("10.9.1.56,password=Password")
    ConnectionMultiplexer.Connect("192.168.0.106:6379,password=1")
);
```

# Как выглядит приложение
<img width="1199" height="885" alt="image" src="https://github.com/user-attachments/assets/a68e0514-1781-4ac2-812e-70ec9aa08ed4" />

