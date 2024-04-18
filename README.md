# Tickets Project

Веб-приложение, в котором организации могут продавать клиентам билеты на свои мероприятия


## Развертывание

Чтобы развернуть проект:  
- Локальный сервис Postgres должен быть выключен, порт 5432 свободен
- Запускаем Docker
- Запускаем файл docker-compose.yml (поднимет базу)
- Запускаем нужное приложение (миграции накатятся автоматически)


## Особенности

### Роли

Пользователь:
- Просматривает предстоящие мероприятия
- Покупает билеты на мероприятия
- Просматривает купленные билеты

Организация:
- Создает и изменяет мероприятия
- Управляет количеством и ценой билетов

### Функционал

Организации:
- Регистрация организации (параметры: название, описание, контактные данные и др.)
- Просмотр организации
- Редактирование организации
- Вход в аккаунт организации

Мероприятия:
- Создание мероприятий (параметры: название, описание, дата и время проведения, фото и др.)
- Просмотр мероприятий
- Редактирование мероприятий

Билеты:
- Создание билетов на мероприятие (параметры: тип, описание, цена, количество билетов, дата и время старта и конца продажи билетов и др.)
- Редактирование билетов

Оплата:
- Создание записи о покупке билетов
- Просмотр покупок (купленных билетов) 
