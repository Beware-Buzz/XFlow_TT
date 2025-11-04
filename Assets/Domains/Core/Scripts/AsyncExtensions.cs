using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public static class AsyncExtensions
    {
        public static async void FireAndForget(this Task task)
        {
            try
            {
                await task;
            }
            catch (OperationCanceledException exception)
            {
                Debug.Log(exception);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception);
            }
        }
    }
}