using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace ProjectName.Shared.ExceptionHandling
{
    public static class ExceptionManager
    {

        public static BaseException HandleException(Exception exceptionToHandle, List<KeyValuePair<string, string>> additionalInfoToLog = null)
        {

            if (exceptionToHandle.GetType() == typeof(BaseException))
            {

                if (additionalInfoToLog != null && additionalInfoToLog.Count > 0)
                {
                    foreach (KeyValuePair<string, string> item in additionalInfoToLog)
                    {

                        exceptionToHandle.Data.Add(item.Key, item.Value);
                    }
                }

                return (BaseException)exceptionToHandle;
            }
            else
            {

                BaseException exceptionToReturn = new BaseException(exceptionToHandle.Message, exceptionToHandle);

                foreach (DictionaryEntry dictionaryEntry in exceptionToHandle.Data)
                {
                    string key = dictionaryEntry.Key.ToString();
                    object value = dictionaryEntry.Value;
                    exceptionToReturn.Data[key] = value;
                }

                StackTrace stackTrace = new StackTrace();
                StackFrame stackFrame = stackTrace.GetFrame(1);
                MethodBase methodBase = stackFrame.GetMethod();
                if (methodBase != null)
                {
                    // string methodName = methodBase.Name;
                    //  string className = methodBase.DeclaringType.Name;

                    exceptionToReturn.Data["class"] = methodBase.Name;
                    exceptionToReturn.Data["method"] = methodBase.DeclaringType.Name;
                }
                if (additionalInfoToLog != null && additionalInfoToLog.Count > 0)
                {
                    foreach (KeyValuePair<string, string> item in additionalInfoToLog)
                    {
                        exceptionToReturn.Data.Add(item.Key, item.Value);
                    }
                }
                exceptionToReturn.Data["ErrorMessage"] = exceptionToHandle.Message;

                return exceptionToReturn;

            }
        }

    }

}