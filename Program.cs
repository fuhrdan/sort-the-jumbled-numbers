//*****************************************************************************
//** 2191. Sort the Jumbled Numbers leetcode                                 **
//** My slow code is at the top, commented out, ChatGPT fast code below -Dan **
//*****************************************************************************


/**
 * Note: The returned array must be malloced, assume caller calls free().
 */
 /*
int* sortJumbled(int* mapping, int mappingSize, int* nums, int numsSize, int* returnSize) {
    int *retnum = (int*)malloc(numsSize * sizeof(int*));
    int current = 0;
    int currentNum = 0;
    int smallNum = 0;
    int reduction = 0;
    int newNum = 0;
    for(int i = 0; i < numsSize; i++)
    {
        currentNum = nums[i];
        smallNum = currentNum;
        newNum = 0;
        while(smallNum >= 10)
        {
            smallNum = smallNum / 10;
            reduction++;
//            printf("small = %d on %d\n",smallNum,reduction);
        }
//        printf("reduction = %d, smallnum = %d",reduction,smallNum);
        while(reduction != 0)
        {
//            printf("newnum = %d + (mapping[%d]=%d * (%d * 10) = ",newNum,smallNum,mapping[smallNum],reduction);
            newNum = newNum + (mapping[smallNum] * (pow(10,reduction)));
//            printf("%d\n",newNum);
            currentNum = currentNum - (smallNum * (pow(10,reduction)));
//            printf("AND Current = %d\n",currentNum);
            smallNum = currentNum;
            reduction = 0;
            while(smallNum >= 10)
            {
                smallNum = smallNum / 10;
                reduction++;
//                printf("small = %d on %d\n",smallNum,reduction);
            }
            if(reduction == 0)
                newNum = newNum + (mapping[smallNum] * (pow(10,reduction)));
        }
//        printf("new = %d\n",newNum);
        retnum[current] = newNum;
        current++;
    }
    
    //Everything is jumbled, now we sort

    for(int i = 0; i < numsSize-1; i++)
    {
        for(int j = i; j < numsSize; j++)
        {
            if(retnum[i] > retnum[j])
            {
                int temp = retnum[i];
                retnum[i] = retnum[j];
                retnum[j] = temp;
                temp = nums[i];
                nums[i] = nums[j];
                nums[j] = temp;
                printf("nums[%d] = %d\n",i,nums[i]);
            }
        }
    }

    for(int i = 0; i < numsSize; i++)
    {
        retnum[i] = nums[i];
        printf("retnum[%d] = %d\n",i,retnum[i]);
    }

    *returnSize = numsSize;
    return retnum;
}
*/

int mapNumber(int num, int* mapping) {
    int result = 0;
    int factor = 1;
    if (num == 0) {
        return mapping[0];
    }
    while (num > 0) {
        int digit = num % 10;
        result += mapping[digit] * factor;
        factor *= 10;
        num /= 10;
    }
    return result;
}

// Comparator function for qsort
int compare(const void* a, const void* b, void* mapping) {
    int num1 = *(int*)a;
    int num2 = *(int*)b;
    int mappedNum1 = mapNumber(num1, (int*)mapping);
    int mappedNum2 = mapNumber(num2, (int*)mapping);
    if (mappedNum1 < mappedNum2) return -1;
    if (mappedNum1 > mappedNum2) return 1;
    return 0;
}

int* sortJumbled(int* mapping, int mappingSize, int* nums, int numsSize, int* returnSize) {
    // Allocate memory for the sorted array
    int* sortedNums = (int*)malloc(numsSize * sizeof(int));
    if (sortedNums == NULL) {
        *returnSize = 0;
        return NULL; // Allocation failed
    }

    // Copy the original nums array to the sorted array
    for (int i = 0; i < numsSize; i++) {
        sortedNums[i] = nums[i];
    }

    // Sort the array using qsort and the custom comparator
    qsort_r(sortedNums, numsSize, sizeof(int), compare, mapping);

    // Set the return size
    *returnSize = numsSize;
    return sortedNums;
}