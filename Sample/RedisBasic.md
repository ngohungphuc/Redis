### 5 data type in redis
- String

- List (order list)

- Set

- Hash

- Sorted Set

#### string get and set
```
set user "name: joe"
get user
```

#### Incrementing
```
set user:id 1
incr user:id
append user:1 "extra data"
```

#### Get Range
```
set customer:1 "ABCD00123"
getrange customer:1 start_index end_indexq
```

#### mset & mget
```
mset order:1 "order 1 data" order:2 "order 2 data"
mget order:1 order:2
```

#### lpush & rpush
```
 lpush recentcomments "comment 1"
 rpush recentcomments "comment 2"
```

#### ltrim

#### adding to a set
```
sadd post:1:likes "joe" "bob"
scard post:1:likes
smembers post:1:likes
sdiff post:1:likes post:2:likes
```