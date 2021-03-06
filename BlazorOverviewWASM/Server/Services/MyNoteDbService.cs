﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorOverviewWASM.Shared.Models;
using BlazorOverviewWASM.Shared.Services;
using Microsoft.EntityFrameworkCore;
using BlazorOverviewWASM.Server.Data;

namespace BlazorOverviewWASM.Server.Services
{
    public class MyNoteDbService: IMyNoteService
    {
        public MyNoteDbContext MyNoteDbContext { get; }
        // 使用建構式注入方式，注入MyNoteDbContext 類別執行個體，以便可以存取SQLite 資料庫
        public MyNoteDbService(MyNoteDbContext myNoteDbContext)
        {
            MyNoteDbContext = myNoteDbContext;
        }
        // 建立一筆新記事紀錄
        public async Task CreateAsync(MyNote myNote)
        {
            await MyNoteDbContext.MyNotes.AddAsync(myNote);
            await MyNoteDbContext.SaveChangesAsync();
        }
        // 查詢所有記事紀錄
        public async Task<List<MyNote>> RetriveAsync()
        {
            return await MyNoteDbContext.MyNotes.ToListAsync();
        }
        // 修改記事紀錄
        public async Task UpdateAsync(MyNote origMyNote, MyNote myNote)
        {
            var fooItem = await MyNoteDbContext.MyNotes.FirstOrDefaultAsync(x => x.Id == origMyNote.Id);
            if (fooItem != null)
            {
                fooItem.Title = myNote.Title;
                await MyNoteDbContext.SaveChangesAsync();
            }
        }
        // 刪除記事紀錄
        public async Task DeleteAsync(MyNote myNote)
        {
            MyNoteDbContext.MyNotes.Remove(await MyNoteDbContext.MyNotes.FirstOrDefaultAsync(x => x.Id == myNote.Id));
            await MyNoteDbContext.SaveChangesAsync();
        }
    }
}
