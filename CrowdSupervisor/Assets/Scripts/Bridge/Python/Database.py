class DatabaseCapturedFrame:

    database = {}

    def add(self, uav_id, data_frame):
        if uav_id in self.database:
            self.database[uav_id] += [data_frame]
        else:
            self.database[uav_id] = [data_frame]

    def getNbrFrame(self):
        values = list(self.database.values())
        return len(values[0])

    def getDatabase(self):
        return self.database

    """def log(self):
        for (key, values) in self.DATABASE.items():
            print(f"Key : {key} - len of list : {len(values)}")
            print("---------")
            for element in values:
                print(f"----- {element[-50:]}")"""