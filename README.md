# ScheduleAggregator

Набросок небольшой тулзы которая вытаскивает расписание и группирует их. Основная цель - набросать механизм, который позволит упростит просмотр пар всего потока с целью поиска "к кому бы пойти на пару, чтобы на работу успеть"

Current state: ИТМО не обновили расписание в API, возвращается расписание за второй семестр прошлого года.

Stack:
- .NET Core 3.0
- [ItmoApiWrapper](https://github.com/InRedikaWB/ItmoApiWrapper) - обертка над API для C#
