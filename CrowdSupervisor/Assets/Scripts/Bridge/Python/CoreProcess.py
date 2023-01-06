from ImageProcessing import ImageProcessing
from Database import DatabaseCapturedFrame

import cv2

class CoreProcess:

    def __init__(self):
        self.imageProcessing = ImageProcessing()
        self.database = DatabaseCapturedFrame()

    def call(self, uav_id, data_frame):
        data_frame = ImageProcessing.formatByteData(data_frame)
        self.database.add(uav_id, data_frame)

        size = self.database.getNbrFrame()
        #print(size)
        if size == 100 and uav_id == 5:
            self.imageProcessing.start(self.database.getDatabase())
