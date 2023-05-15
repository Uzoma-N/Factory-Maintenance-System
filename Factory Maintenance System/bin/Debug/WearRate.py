# -*- coding: utf-8 -*-
"""
Created on Tue Apr 21 18:37:52 2020

@author: uzoma
"""


def WearRate(Pressure, distance):
    k = 1.7 * (10 **-5)    #wear coefficient
    sumPressure = 0
    sumdistance = 0

    if len(Pressure) > 30:

        for i in range(len(Pressure) - 30, len(Pressure)):
            sumPressure += float(Pressure[i])

        for j in range(len(distance) - 30, len(distance)):
            sumdistance += float(distance[j])

        avPressure = sumPressure / 30.0
        avdistance = sumdistance / 30.0

    else:
        for i in range(len(Pressure)):
            sumPressure += float(Pressure[i])

        for j in range(len(distance)):
            sumdistance += float(distance[j])

        avPressure = sumPressure / len(Pressure)
        avdistance = sumdistance / len(distance)

    return k * avPressure * avdistance
