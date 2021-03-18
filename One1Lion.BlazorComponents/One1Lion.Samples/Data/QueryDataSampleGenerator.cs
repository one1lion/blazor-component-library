using One1Lion.Samples.SharedLib.DbInfo;
using One1Lion.Samples.SharedLib.Search.DBExpressions;
using One1Lion.Samples.SharedLib.Search.FileRepositoryExpressions;
using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace One1Lion.Samples.Data {
  public class QueryDataSampleGenerator {
    public static QueryExpressionGroup GenerateQueryExpressionGroup() {
      var baseGroup = new QueryExpressionGroup();
      var sqlGroup = new DBExpressionGroup() {
        Name = "SQL"
      };
      sqlGroup.AddChild(new DBExpressionItem() {
        ExpressionType = ExpressionType.EqualTo,
        QueryField = "DocType",
        Values = new List<object> { "RPT" }
      });
      var group1 = new DBExpressionGroup();

      group1.AddChild(new DBExpressionItem() {
        ExpressionType = ExpressionType.ContainsPhrase,
        QueryField = "DocTitle",
        Values = new List<object> { "contaminant" }
      });

      group1.AddChild(new DBExpressionItem() {
        ExpressionType = ExpressionType.After,
        QueryField = "DateEntered",
        Values = new List<object> { new DateTime(2019, 1, 1) },
        AndWithNext = false

      });

      group1.AddChild(new DBExpressionItem() {
        ExpressionType = ExpressionType.GreaterThan,
        QueryField = "DocDate",
        Values = new List<object> { "20190101" }
      });

      sqlGroup.AddChild(group1);
      var group2 = new DBExpressionGroup();

      group2.AddChild(new DBExpressionItem() {
        ExpressionType = ExpressionType.Between,
        QueryField = "DocDate",
        Values = new List<object> { "20190101", "20190201" }
      });

      var group2_1 = new DBExpressionGroup() {
        Children = new List<IQueryElement>() {
          new ContainsPhraseExpression() {
            QueryField = "DocTitle",
            Values = new List<object> { "change notice" },
            AndWithNext = false
          },
          new ContainsAnyExpression() {
            QueryField = "DocTitle",
            Values = new List<object> { "revision b", "rev b", "rev. b", "rev.b" }
          }
        },
        Parent = sqlGroup
      };

      group2.AddChild(group2_1);

      group2.AddChild(new DBExpressionItem() {
        ExpressionType = ExpressionType.ContainsPhrase,
        QueryField = "DocTitle",
        Values = new List<object> { "change notice" }
      });

      group2.AddChild(new DBExpressionItem() {
        ExpressionType = ExpressionType.ContainsAny,
        QueryField = "DocTitle",
        Values = new List<object> { "revision b", "rev b", "rev. b", "rev.b" }
      });

      sqlGroup.AddChild(group2);
      baseGroup.AddChild(sqlGroup);
      var group3 = new DBExpressionGroup() {
        Name = "SQL"
      };
      baseGroup.AddChild(group3);

      var dtSearchGroup = new FileRepositoryExpressionGroup() {
        Name = "dtSearch"
      };
      dtSearchGroup.AddChild(new FileRepositoryExpressionItem());
      group2_1.AddChild(dtSearchGroup);

      return baseGroup;
    }

    public async static Task<List<DbFieldViewModel>> GenerateQueryableFields(HttpClient httpClient) {
      return await httpClient.GetFromJsonAsync<List<DbFieldViewModel>>("sample-data/QueryableFields.json");
    }
  }
}
